using Microsoft.Extensions.DependencyInjection;
using StartFromScratch.Models;

var builder = WebApplication.CreateBuilder(args);


// Mettre à disposition mes controllers et les vues dans l'injection de dépendance
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<string>("toto");

// Cet objet sera celui renvoyé par DI si demande pour List<Employe>


var listeDesEmployes = new List<Employe>()
        {
            new Employe(){Nom="Mauras", Prenom="Dominique", Actif=true, DateEntree=DateTime.Now, Matricule="007", Salaire=1000000000},
            new Employe(){Nom="Gates", Prenom="Bill", Actif=true, DateEntree=DateTime.Now, Matricule="009", Salaire=100000000},
            new Employe(){Nom="Waine", Prenom="John", Actif=false, DateEntree=DateTime.Now, Matricule="005", Salaire=1000000}
        };

// Mode singleton => Toute demande de List<Employe> sera résolmue en renvoyant l'objet listeDesEmployes
//builder.Services.AddSingleton<List<Employe>>(listeDesEmployes);

// Ici, DI attend que une demande de List<Employe> arrive
// Exécute la fonction => stocke l'objet retourné pour les demandes suivantes
builder.Services.AddSingleton<List<Employe>>((s)=> new List<Employe>()
        {
            new Employe(){Nom="Mauras", Prenom="Dominique", Actif=true, DateEntree=DateTime.Now, Matricule="007", Salaire=1000000000},
            new Employe(){Nom="Gates", Prenom="Bill", Actif=true, DateEntree=DateTime.Now, Matricule="009", Salaire=100000000},
            new Employe(){Nom="Waine", Prenom="John", Actif=false, DateEntree=DateTime.Now, Matricule="005", Salaire=1000000}
        });




var app = builder.Build();

// Création d'une route
// Association entre forme url et les méthodes de controller
// /Employe/Addition => {controller} => Employe , {action} => Addition
app.MapControllerRoute("default", "{controller}/{action}", new {Controller="Employe", Action="Index"});
// Association entre url / et le controller / action souhaité
app.MapControllerRoute("root", "/",new  { Controller="Employe", Action= "Addition" });
//app.MapGet("/", () => "Hello World!");

app.Run();
