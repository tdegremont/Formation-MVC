using Microsoft.Extensions.DependencyInjection;
using StartFromScratch.Models;

var builder = WebApplication.CreateBuilder(args);

// Injection de d�pendance 
// Syst�me qui permet d'obtenir des instances de classe 
// 1) Controle du nombre d'instances cr��es
// 2) Faible couplage => Choisir une classe en uutilisant un fichier de config

// Mettre � disposition mes controllers et les vues dans l'injection de d�pendance
// AddControllersWithViews est fournie par le package MVC
builder.Services.AddControllersWithViews();
// builder.Services.AddSingleton<string>("toto");

// Cet objet sera celui renvoy� par DI si demande pour List<Employe>


//var listeDesEmployes = new List<Employe>()
//        {
//            new Employe(){Nom="Mauras", Prenom="Dominique", Actif=true, DateEntree=DateTime.Now, Matricule="007", Salaire=1000000000},
//            new Employe(){Nom="Gates", Prenom="Bill", Actif=true, DateEntree=DateTime.Now, Matricule="009", Salaire=100000000},
//            new Employe(){Nom="Waine", Prenom="John", Actif=false, DateEntree=DateTime.Now, Matricule="005", Salaire=1000000}
//        };

// Mode singleton => Toute demande de List<Employe> sera r�solmue en renvoyant l'objet listeDesEmployes
//builder.Services.AddSingleton<List<Employe>>(listeDesEmployes);

// Ici, DI attend que une demande de List<Employe> arrive
// Ex�cute la fonction => stocke l'objet retourn� pour les demandes suivantes
builder.Services.AddSingleton<List<Employe>>((s)=> new List<Employe>()
        {
            new Employe(){Nom="Mauras", Prenom="Dominique", Actif=true, DateEntree=DateTime.Now, Matricule="007", Salaire=1000000000},
            new Employe(){Nom="Gates", Prenom="Bill", Actif=true, DateEntree=DateTime.Now, Matricule="009", Salaire=100000000},
            new Employe(){Nom="Waine", Prenom="John", Actif=false, DateEntree=DateTime.Now, Matricule="005", Salaire=1000000}
        });




var app = builder.Build();

// Cr�ation d'une route
// Association entre forme url et les m�thodes de controller
// /Employe/Addition => {controller} => Employe , {action} => Addition
app.MapControllerRoute("default", "{controller}/{action}/{id}", new {Controller="Employe", Action="Index", Id=""});
// Association entre url / et le controller / action souhait�
app.MapControllerRoute("root", "/",new  { Controller="Employe", Action= "Addition" });
//app.MapGet("/", () => "Hello World!");

app.Run();
