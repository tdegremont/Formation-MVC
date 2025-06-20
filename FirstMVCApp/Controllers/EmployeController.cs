using FirstMVCApp.Services;
using Microsoft.AspNetCore.Mvc;
using FirstMVCApp.Models;
using FirstMVCApp.ViewModels.Employe;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.AspNetCore.Authorization;

namespace FirstMVCApp.Controllers
{
    // Controller destiné à géréer les fonctionnalités offertes pour les employés
    // Attribut Controller => Identifie une class comme étant un controller
    // Services.AddController le prendra en compte
    [Controller]
    // Cet attribut check les AntiforgeryTokens de toutes les méthodes sauf GET
    //[AutoValidateAntiforgeryToken]
    public class EmployeController : Controller
    {
        private readonly IEmployeService employeService;


        // Dans le constructeur je demande à l'injection de dépendance de me donner la liste des employes
        public EmployeController([FromServices] IEmployeService employeService,
            [FromServices] ILogger<EmployeController> logger

            )
        {
            logger.LogWarning("Construction de EmployeController");
            this.employeService = employeService;
            // Je la stocke localement pour utilisation dans les action

        }


        // Créer une action qui affiche dans une page HTML 
        // 1) les employes
        // 2) le montant de charges salariales de l'entreprise
        // GET : /Employe/Index
        // [FromForm] EmployeSearchModel searchModel => Je demande au binder 
        // de constitueer un objet EmployeSearchModel à partir des éléments envoyés
        // dans la partie Form de la requete
        public async Task<IActionResult> Index(EmployeSearchModel searchModel)
        {
            if (Request.Method == "GET")
            {
                ModelState.Clear();
            }

            // ModelState donne des indications sur la validation du modèle
            // Ici je peux agir en fonction de la validation
            if (!ModelState.IsValid)
            {
                return View(new IndexVM() { SearchModel = searchModel });
            }
            // InEnumerable => Resultats non matérialisés
            var employes = await employeService.GetEmployesAsync(searchModel);

            // LMatérialisation (utile si je suis sûr que la vue va énumérer)
            var listeEmployes = employes.ToList();
            var model = new IndexVM()
            {
                Liste = listeEmployes,
                MasseSalariale = listeEmployes.Where(c => c.Actif).Sum(c => c.Salaire),
                // Cette propriété du ViewModel va permettre à la vue d'afficher les critères de recherce
                SearchModel = searchModel
            };

            //ViewBag.UserName = "Dominique";
            //ViewData["UserName"] = "Dominique";


            return View(model);
        }


        // GET : Employe/AugmenterSalaires => Augmenter les salaires de 10%
        // Réafficher les employes + masse salariale
        // return View("Index",model)
        public async Task<IActionResult> AugmenterSalaires(
            EmployeSearchModel searchModel,
            decimal taux,
            [FromServices] IConfiguration config
            )
        {
            if (config.GetSection("Mode").Value == "Ram") { }


            // Augpenter les salaires grace au service
            var employesAugmentes = await employeService.AugmenterEmployesAsync(searchModel, taux);

            var model = new IndexVM()
            {
                Liste = employesAugmentes,
                MasseSalariale = employesAugmentes.Where(c => c.Actif).Sum(c => c.Salaire),
                // Cette propriété du ViewModel va permettre à la vue d'afficher les critères de recherce
                SearchModel = searchModel
            };
            return RedirectToAction("Index");
            return View("Index", model);


            //var model = new IndexVM()
            //{
            //    Liste = dataEmployes,
            //    MasseSalariale = dataEmployes.Where(c => c.Actif).Sum(c => c.Salaire)
            //};
            //return View("Index", model);
        }


        // GET : /Employe/Details/003
        public async Task<IActionResult> Details([FromRoute(Name = "id")] string matricule)
        {
            var employe = await employeService.GetEmployeAsync(matricule);
            if (employe == null)
            {
                // Erreur 404 => Pourra être attrapée par une page élégante par défaut
                // return NotFound();
                return RedirectToAction("Index");
            }
            // /Views/Employe/Details
            return View(employe);
        }


        // Méthode du controller (public sinon ne fonctionne pas)
        // Get /Employe/Addition
        public IActionResult Addition(int a, int b)
        {
            // model = les données envoyées à la vue
            var model = new AdditionVM { A = a, B = b, Result = a + b };
            // retour d'un ViewResult => Html généré par la vue
            // La vue utilisée est /Views/Employe/Addition.cshtml
            return View(model);
        }



        // Certaines action sont réalisées en deux phase
        // 1) Affichage d'une page de validation (sur requete GET contenant un formulaire
        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute(Name = "id")] string matricule)
        {
            var employe = await employeService.GetEmployeAsync(matricule);
            return View(employe);
        }

        // 2) Réalisation de la suppression sur retour du formulaire de la phase 1
        [HttpPost]

        // Action filter => Attribut qui va empêcher une action 
        // de s'éxécuter sous certaines condition

        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute(Name = "id")] string matricule, bool validation)
        {
            await employeService.DeleteEmployeAsync(matricule);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Create(Employe employe)
        {


            if (string.IsNullOrEmpty(employe.Matricule))
            {
                employe.Matricule = GetNextMatricule(employeService);
            }
            return View(employe);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employe employe, bool validation)
        {
            if (!ModelState.IsValid) { return View(employe); }
            await employeService.CreateEmployeAsync(employe);
            return RedirectToAction("Index");
        }

        public string GetNextMatricule([FromServices] IEmployeService employeService)
        {
            var employes = employeService.GetEmployesAsync(new EmployeSearchModel()).Result;
            return (employes.Select(c => int.Parse(c.Matricule)).Max() + 1).ToString().PadLeft(3, '0');
        }
    }
}
