using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net;

namespace FirstMVCApp.CustomAttributes.AuthorizeFilters
{
    /// <summary>
    /// Classe de filtre destinée à autoriser / interdire l'exécution d'une action / controlleur
    /// </summary>
    public class HeuresBureauFilter : IAuthorizationFilter
    {
        private readonly int h1;
        private readonly int h2;
        private readonly IConfiguration config;

        public HeuresBureauFilter(int h1,int h2,IConfiguration config)
        {
            this.h1 = h1;
            this.h2 = h2;
            this.config = config;
        }
        public void OnAuthorization(AuthorizationFilterContext context
            
            )
        {
            if (DateTime.Now.Hour < h1 || DateTime.Now.Hour >= h2) {
                var viewData = new ViewDataDictionary(
                    new EmptyModelMetadataProvider(), 
                    context.ModelState
                    ) { 
                        // Je définis le modèle pour la vue que je vais afficher
                        Model = (h1, h2) 
                };
                // J'ajoute des éléments dans le ViewBag si nécessaire
                viewData["Title"] = "Erreur d'horaire";

          
                context.Result = new ViewResult() { ViewName = "/Views/Errors/HeuresBureauError.cshtml" , ViewData=viewData};
            }
        }


    }
}
