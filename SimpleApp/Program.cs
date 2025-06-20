using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<String>("Toto");
var app = builder.Build();

// Middleware répond à l'url / et renvoit au client "Hello World!" => ajoutés en fin de chaine
app.MapGet("/", () => "Hello World!");
app.MapGet("/toto", ([FromServices]string a) =>
{
    return "Hello World! Toto";
});


// Premier middleware
app.Use(async (HttpContext context, Func<Task> next)=>
{
    // context : contient les informations sur la requette + l'objet reponse
    // next => fonction qui permet d'exécuter le code des middleware qui suivent
    Console.WriteLine("Entree de requete : " + context.Request.Path);
    var dateEntree = DateTime.Now;
    // Execution des traitements des autres middlewares
    await next(); // Traitement peut prendre du temps
    var time = (DateTime.Now - dateEntree).Microseconds;
    Console.WriteLine("Fin de requete : " + context.Request.Path+" en "+time.ToString()+" microsecondes");
});


// Middleware qui empeche les requêtes d'aboutir les minutes paire
app.Use(async (HttpContext context, Func<Task> next )=>
{
    if (DateTime.Now.Minute % 2 == 0)
    {
        await context.Response.WriteAsync("Je me repose les minutes paires");
    }
    else
    {
        await next();
    }
});


app.Run();
