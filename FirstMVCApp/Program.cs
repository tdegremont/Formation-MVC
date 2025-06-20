
using FirstMVCApp.Services;
using FirstMVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using FirstMVCApp;
using FirstMVCApp.CustomAttributes.AuthorizeFilters;
using FirstMVCApp.Services.DALEmployes;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
// Système de mappage entre les différent type de classe
// DAO <=> Model

builder.Services.AddAutoMapper(config =>
{
    // Permet au mapper de mapper propriété par propriété de même nom
    config.CreateMap<Employe, EmployeDAO>()
    // Pour les propriétés qui ont des nom différents, on indique l'association
    .ForMember(c=>c.Name,o=>
    {
        o.MapFrom(c => c.Nom);
        o.NullSubstitute("");
    })
    
    .ReverseMap();

});







// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    // Tous les controllers auront le ActionFilter qui check le AntiforgeryToken si pas requete GET
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

    // Application globale d'un filtre 
    // options.Filters.Add(new EmployeServiceExceptionFilter());
});

var modeFonctionnementEmployes = builder.Configuration.GetSection("Mode").Value;
//Ajout d'une d�pendance associ�e � une demande de service sur IEmployeService
// Retourne un EmployeServiceFromList en mode singleton




builder.Services.AddSingleton<List<Employe>>((s) => new List<Employe>()
            {
                new Employe(){Nom="Mauras", Prenom="Dominique", Actif=true,
                    DateEntree=DateTime.Now.AddYears(-7),
                    Matricule="007", Salaire=1000000000},
                new Employe(){Nom="Gates", Prenom="Bill", Actif=true,

                    DateEntree=DateTime.Now.AddYears(-6), Matricule="009",
                    Salaire=100000000},
                new Employe(){Nom="Waine", Prenom="John", Actif=false,
                    DateEntree=DateTime.Now, Matricule="005", Salaire=1000000}
            });

switch (builder.Configuration.GetSection("Mode").Value)
{
    case "RAM":

        builder.Services.AddSingleton<IEmployeService, EmployeServiceFromList>();
        break;

    case "BDD":
        // Demander la création de la BDD si elle n'existe pas 


        // AddSingleton => 1 seule instance pour tous
        // AddTransient => 1 instance par demande
        // AddScoped => avec MVC => 1 instance par requète
        // Un contexte va interroger la bdd avec une authentification qui
        // peut être spécifique à l'utilisateur connecté
        builder.Services.AddScoped<IEmployeService, EmployeServiceFromDB>();
        // Ajout du DbContext nécessaire au service EmployeServiceFromDB
        builder.Services.AddDbContext<EmployeDbContext>(options =>
        {
            // Spécification des options
            // Provider : Ajouter le package du provider
            // Ajouter la chaine de connection dans la config (appsettings.json)
            options.UseSqlServer("name=EmployeConnection");
        });

        break;
    default:
        break;
}



var app = builder.Build();
//app.UseExceptionHandler("/Home/Error");


// Vérifie la présence de la BDD au démarrage du server
// si on est en mode BDD
if (app.Configuration.GetSection("Mode").Value == "BDD")
{
    // Demande un objet de type EmployeDbContext
    // DbContext est en mode AddScoped
    // Il faut créer un scope pour demander l'objet
    using (var scope = app.Services.CreateScope())
    {
        // Je demande au scope un objet de type EmployeDbContext
        var context = scope.ServiceProvider.GetService<EmployeDbContext>();
        // J'utilise l'objet pour créer la BDD si elle n'existe pas
        context.Database.EnsureCreated();

    }
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employe}/{action=Index}/{id?}");

app.Run();
