using FirstMVCApp.Services;
using Microsoft.AspNetCore.Mvc;
using FirstMVCApp.Models;
using FirstMVCApp.ViewModels.Employe;
using FirstMVCApp.CustomAttributes.AuthorizeFilters;
using FirstMVCApp.CustomAttributes.ExceptionFilters;
using FirstMVCApp.CustomAttributes.ActionFilters;


namespace FirstMVCApp.Controllers
{
    // Controller destiné à géréer les fonctionnalités offertes pour les employés
    // Attribut Controller => Identifie une class comme étant un controller
    // Services.AddController le prendra en compte
    [Controller]


    // Cet attribut check les AntiforgeryTokens de toutes les méthodes sauf GET
    //[AutoValidateAntiforgeryToken]



    // TypeFilter : Appliquer un filtre sur un controller
    // [TypeFilter(typeof(EmployeServiceExceptionFilter))]

    // Filtres appliqués au controller (voir dossier CustomAttributes)
    [EmployeServiceException] //=> ExceptionFilter
    [HeuresBureau(9,18)] // => AuthorizationFilter
    [LogFilter("Avant {1} dans {0}", "Après {1} dans {0}")] // => ActionFilter
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


        // Employe/Index
        // Employe/index?partial=true
        public async Task<IActionResult> Index( EmployeSearchModel searchModel, bool partial=false, int? page=null)
        {
            if (Request.Method == "GET")
            {
                ModelState.Clear();
            }

            // ModelState donne des indications sur la validation du modèle
            // Ici je peux agir en fonction de la validation
            if (!ModelState.IsValid)
            {
                return View(new IndexVM() { SearchModel = searchModel});              
            }
            // InEnumerable => Resultats non matérialisés
            var employes = await employeService.GetEmployesAsync(searchModel);

            // LMatérialisation (utile si je suis sûr que la vue va énumérer)
            var listeEmployes = employes.Skip((page==null ? 0 : (page.Value-1)*10)).ToList();
            var model = new IndexVM()
            {
                Liste = listeEmployes ,
                MasseSalariale = listeEmployes.Where(c => c.Actif).Sum(c => c.Salaire),
                // Cette propriété du ViewModel va permettre à la vue d'afficher les critères de recherce
                SearchModel = searchModel
            };

            //ViewBag.UserName = "Dominique";
            //ViewData["UserName"] = "Dominique";

            if (!partial)
            {
                // Retour normal (avec layout)
                return View(model);
            }
            else
            {
                // Retour minimal (juste les employes) grâce à la vue partielle créée 
                // qui ne renvoit que la liste des employé
                // sans layout
                return PartialView("_Index_Partial",model.Liste);
            }
        
        }


        public async Task<ActionResult> IndexDynamique(EmployeSearchModel searchModel)
        {
            var model = new IndexVM()
            {

                SearchModel = searchModel
            };
            return View(model);
        }



        // GET : Employe/AugmenterSalaires => Augmenter les salaires de 10%
        // Réafficher les employes + masse salariale
        // return View("Index",model)
        public async Task< IActionResult> AugmenterSalaires(
            EmployeSearchModel searchModel,
            decimal taux,
            [FromServices] IConfiguration config
            )
        {
            if (config.GetSection("Mode").Value == "Ram") { }


            // Augpenter les salaires grace au service
            var employesAugmentes=await employeService.AugmenterEmployesAsync(searchModel, taux);

            var model = new IndexVM()
            {
                Liste = employesAugmentes,
                MasseSalariale = employesAugmentes.Where(c => c.Actif).Sum(c => c.Salaire),
                // Cette propriété du ViewModel va permettre à la vue d'afficher les critères de recherce
                SearchModel = searchModel
            };
            return RedirectToAction("Index");
            return View("Index", model );


            //var model = new IndexVM()
            //{
            //    Liste = dataEmployes,
            //    MasseSalariale = dataEmployes.Where(c => c.Actif).Sum(c => c.Salaire)
            //};
            //return View("Index", model);
        }


        // GET : /Employe/Details/003
        public async Task<IActionResult> Details([FromRoute(Name ="id")] string matricule)
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
        public IActionResult Addition(int a, int b) {
            // model = les données envoyées à la vue
            var model = new AdditionVM { A = a, B = b, Result = a + b };
            // retour d'un ViewResult => Html généré par la vue
            // La vue utilisée est /Views/Employe/Addition.cshtml
            return View(model);
        }



        // Certaines action sont réalisées en deux phase
        // 1) Affichage d'une page de validation (sur requete GET contenant un formulaire
        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute(Name ="id")]string matricule)
        {
            var employe = await employeService.GetEmployeAsync(matricule);
            return View(employe);
        }

        // 2) Réalisation de la suppression sur retour du formulaire de la phase 1
        [HttpPost]

        // Action filter => Attribut qui va empêcher une action 
        // de s'éxécuter sous certaines condition
       
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute(Name = "id")] string matricule,bool validation)
        {
            await employeService.DeleteEmployeAsync(matricule);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Dans la réalité : ermployeService.GetNewEmploye()
            var e = new Employe() { Actif = true, Salaire = 1000, DateEntree=DateTime.Now };
            return View(e);
        }


        [HttpPost]
        [TypeFilter(typeof(HeuresBureauFilter))]
        // Bind permet d'éviter la surchage du formulaire
        // Seules les propriétés incluses dans Bind seront prises à partir de la requète
        public async Task<IActionResult> Create(Employe e)
        {

            if (!ModelState.IsValid) {
                return View(e);
            }
            // Dans la réalité : ermployeService.GetNewEmploye()
            var employeCompleteParService = employeService.AddEmployeAsync(e);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> ErreursClassiques(EmployeSearchModel searchModel)
        {
            // Le code écrit ici est le code qui fait effectivement l'action pour l'utilisateur

            // Non, mieux sous forme de  Authorization Attribute / Filter
            if(DateTime.Now.Hour<8 || DateTime.Now.Hour < 19)
            {
                return RedirectToAction("Home");
            }
            // Journalisation de l'action => Non, mieux un action Filter / Attribute
            // Try catch => Non, mieux sous forme d'un Exception filter / Attribute

            return View("toto");
        }
        }
}
