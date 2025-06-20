namespace Code_tests;

[TestClass]
public class Attributes
{
    [TestMethod]
    public void AttributeTests()
    {
        // Objet de la class Toto
        var o = new Toto();
        var typeDeO= o.GetType();

        // J'obtiens les infos sur la propriété Age
        var proprieteAge = typeDeO.GetProperty("Age");
       

        var etiquetteAttributePourAge=proprieteAge.GetCustomAttributes(typeof(EtiquetteAttribute), true).FirstOrDefault();
        if (etiquetteAttributePourAge != null)
        {
            var key = ((EtiquetteAttribute)etiquetteAttributePourAge).Key;
        }
    }


}

// Attribut / Annotation => Finit par Attribute + Hérite de Attribute
/// <summary>
/// Cet attribut indique que une propriété doit avoir un mabel dans une vue
/// Le texte du label doit être recherche dans un dictionnaire de traduction
/// avec la clé Age
/// </summary>
class EtiquetteAttribute : Attribute
{
    public EtiquetteAttribute(string key)
    {
        Key = key;
    }

    public string Key { get; }
}




class Toto
{

    [Etiquette("Age")]

    public int Age { get; set; }
}