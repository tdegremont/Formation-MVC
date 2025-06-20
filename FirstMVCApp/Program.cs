
using FirstMVCApp.Services;
using FirstMVCApp.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "fr-FR", "fr" };
    options.SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
});


// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    // Tous les controllers auront le ActionFilter qui check le AntiforgeryToken si pas requete GET
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});

var modeFonctionnementEmployes = builder.Configuration.GetSection("Mode").Value;
if ( modeFonctionnementEmployes== "RAM")
{
    builder.Services.AddSingleton<List<Employe>>((s) => new List<Employe>()
            {
                new Employe(){Nom="Mauras", Prenom="Dominique", Actif=true, 
                    DateEntree=DateTime.Now.AddYears(-7), 
                    Matricule="007", Salaire=1000000000},
                new Employe(){Nom="Gates", Prenom="Bill", Actif=true, 
                    
                    DateEntree=DateTime.Now.AddYears(-6), Matricule="009", 
                    Salaire=100000000},
                new Employe(){Nom="Waine", Prenom="John", Actif=false, DateEntree=DateTime.Now, Matricule="005", Salaire=1000000}
            });

    //Ajout d'une dépendance associée à une demande de service sur IEmployeService
    // Retourne un EmployeServiceFromList en mode singleton

    builder.Services.AddSingleton<IEmployeService, EmployeServiceFromList>();
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseRequestLocalization(new RequestLocalizationOptions
{
    ApplyCurrentCultureToResponseHeaders = true
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
