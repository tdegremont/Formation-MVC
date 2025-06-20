namespace Code_tests;

[TestClass]
public sealed class Tests
{



    [TestMethod]
    public void DecimalTest()
    {
        double a = 0;
        double increment = 0.3;
        for (int i = 0; i < 100; i++)
        {
            a += increment;
        }

        if (a == 30)
        {
            Console.WriteLine("Ok");
        }


    }

    [TestMethod]
    public void DelegateTest()
    {
        int result = 1;
        result = Addition(1, 2);

        // la variable f est définie comme de type
        // fonction avec deux paramètre de type entier et un return de type entiie
        Func<int, int, int> f = Addition; // f est un pointeur vers Addition
        result = f(1, 2); // 3

        f = (a, b) => a - b; // Fonction lambda ou fléchée
        result = f(1, 2); //-1

        f = (a, b) =>
        {
            return a * b;
        };
        f = (int a, int b) =>
        {
            return a / b;
        };




    }

    [TestMethod]
    public void CalculUse()
    {
        // instanciation fortement couplée 
        // PlanService calculService = new PlanService();

        // Instanciation faiblement couplée
        // Dans le code que j'écris, je ne fais pas référence à une classe
        // mais à une interface
        // Services représente l'injection de dépendance (inclus dans app mvc)
        // Quelque part dans un fichier de config
        // ICalculs => PlanService
     //   ICalculs calculService = Services.Get<ICalculs>();
        // En fonction de la config, l'injection de dépendances (Services)
        // Instanciera une instance de la classe inscrite dans le fichier de config

        // reste du code
        // var result= calculService.GetDistance(1, 2);
  }


    // Addition : fonction codée de ma nière déclarative
    // Addition pointe vers un morceau de code
    int Addition(int a, int b)
    {
        return a + b;
    }
    


}

// Interface = contrat
interface ICalculs
{
    // Méthode imposée
    double GetDistance(double a, double b);
}

class PlanService : ICalculs
{
    public double GetDistance(double a, double b)
    {
        return b - a;
    }
}

class SphereService : ICalculs
{
    public double GetDistance(double a, double b)
    {
        return (b - a) *0.98;
    }
}


class SpaceService 
{
    public double GetDistanc(double a, double b)
    {
        return (b - a) *0.98;
    }
}
