using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace FirstMVCApp.CustomAttributes.AuthorizeFilters
{
    // Attribut qui permet d'associer le filtre au niveau d'une action ou contrôleur
    public class HeuresBureauAttribute : TypeFilterAttribute
    {
        // Constructeur qui :
        // utilise le constructeur de base pour spécifier le type du filtre
        // spécifie (Arguments) les premiers arguments à utiliser dans la construction du filtre
        // Le TypeFilterAtrtribute fournira les autres arguments par DI
        public HeuresBureauAttribute(int h1=8,int h2=20):base(typeof(HeuresBureauFilter))
        {
            this.Arguments=new object[]{h1,h2};
        }
    }
}
