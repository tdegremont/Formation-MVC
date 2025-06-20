
using FirstMVCApp.Services;
using StartFromScratch.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var modeFonctionnementEmployes = builder.Configuration.GetSection("Mode").Value;
if ( modeFonctionnementEmployes== "RAM")
{
    builder.Services.AddSingleton<List<Employe>>((s) => new List<Employe>()
            {
                new Employe(){Nom="Mauras", Prenom="Dominique", Actif=true, DateEntree=DateTime.Now, Matricule="007", Salaire=1000000000},
                new Employe(){Nom="Gates", Prenom="Bill", Actif=true, DateEntree=DateTime.Now, Matricule="009", Salaire=100000000},
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
