using Microsoft.AspNetCore.Mvc;
using StartFromScratch.Models;
using StartFromScratch.ViewModels.Employe;

namespace StartFromScratch.Controllers
{
    // Controller destiné à géréer les fonctionnalités offertes pour les employés
    // Attribut Controller => Identifie une class comme étant un controller
    // Services.AddController le prendra en compte
    [Controller]
 
    public class EmployeController : Controller
    {
        private readonly List<Employe> dataEmployes;

        // Dans le constructeur je demande à l'injection de dépendance de me donner la liste des employes
        public EmployeController([FromServices] List<Employe> dataEmployes)
        {
            // Je la stocke localement pour utilisation dans les action
            this.dataEmployes = dataEmployes;
        }


        // Créer une action qui affiche dans une page HTML 
        // 1) les employes
        // 2) le montant de charges salariales de l'entreprise
        // GET : /Employe/index
        public IActionResult Index()
        {
            var model = new IndexVM()
            {
                Liste = dataEmployes,
                MasseSalariale = dataEmployes.Where(c => c.Actif).Sum(c => c.Salaire)
            };
            return View(model);
        }


        // GET : Employe/AugmenterSalaires => Augmenter les salaires de 10%
        // Réafficher les employes + masse salariale
        // return View("Index",model)
        public IActionResult AugmenterSalaires()
        {
            // Augpenter les salaires de la liste de 10%
            foreach(var e in dataEmployes)
            {
                e.Salaire *= 1.1M;
            }
            var model = new IndexVM()
            {
                Liste = dataEmployes,
                MasseSalariale = dataEmployes.Where(c => c.Actif).Sum(c => c.Salaire)
            };
            return View("Index", model);
        }


        // Méthode du controller (public sinon ne fonctionne pas)
        // Get /Employe/Addition
        public IActionResult Addition(int a, int b) {
            // model = les données envoyées à la vue
            var model = new AdditionVM { A = a, B = b, Result = a + b };
            // retour d'un ViewResult => Html généré par la vue
            // La vue utilisée est /Views/Employe/Addition.cshtml
            return View(model);
        }
    }
}
