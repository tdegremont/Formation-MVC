namespace Code_tests;

[TestClass]
public class IEnumerableTests
{
    [TestMethod]
    public void IEnumerableTest()
    {
        IEnumerable<char> list = new List<char>() { 'a', 'b', 'z' };
        list = "Toto";
        list=new char[] { 'a', 'b', 'z' };

        // IEnumerable impose une fonction GetEnumerator
        var enumerator=list.GetEnumerator();
        while (enumerator.MoveNext()) { 
            var e=enumerator.Current;
            Console.WriteLine(e);   
        }

        foreach(var e in list)
        {
            Console.WriteLine(e);
        }

        var listeChar = "aczfs".OrderBy(c => c); //  acfsz

        var entiers = new List<int>() { 5, 9, 1, 3, 6 };

        // Je ne créé pas d'objet en méméoire avec les résultats du filtre
        // Je créé uniquement une "Vue" correspondant au filtrage
        // Pas de RAM mais refiltrage à chaque énumation
        var petitsEntiers = entiers.Where(c => 
                    c < 6); // 5,1,3

        // je filtre et je stocke dans une liste
        // Prend de la mémoire mais le filtrage est effectué de manièere définitive
        var petitsEntiersMatérialisés = entiers.Where(c =>
            c < 6).ToList(); // 5,1,3

        var c = petitsEntiers.Count(); // 3

        entiers.Add(4);
         c = petitsEntiers.Count(); // 4
        c = petitsEntiersMatérialisés.Count(); // 3 
    }


    [TestMethod]
    public void YieldReturn()
    {
        var liste = TousLesEntiersPositifs();
        // Iterateur => demande les éléments de la liste 1 par 1
        foreach (var e in liste.Take(1000))
        {

        }
    }


    // Generator => renvoit les élément 1 par 1
    IEnumerable<int> TousLesEntiersPositifs()
    {
        //return new List<int>() { 1, 2, 3, 4 };
        int i = 0;
        while (true)
        {
            yield return i;
            i++;

        }
    }
}
