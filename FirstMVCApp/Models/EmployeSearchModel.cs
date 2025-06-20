using FirstMVCApp.CustomAttributes.ValidationAttributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FirstMVCApp.Models
{
    // ce modèle sert à rechercher un employe
    // => générer un formulaire de recherche
    public class EmployeSearchModel : IValidatableObject
    {
        [DisplayName("Matricule, Nom ou Prénom")]
        //[Required(ErrorMessage ="{0} est requis")]
        // Recherche dans matricule, nom, prenom
        public string? Texte { get; set; }

        // Attribut personnalisé de validation
        // Le constructeur demande le diviseur
        // et je spécifie la valeur de errorMessage
        [EstDivisiblePar(2,errorMessage:"{0} doit être un multiple de {1}")]
        [DisplayName("Ancienneté")]
        [Range(1, 70, ErrorMessage = "{0} doit être entre {1} et {2}")]
        // Par anciennete minimale en années
        public int? Anciennete { get; set; } 

        // Cette méthode sera appelée par MVC
        // Pour valider les données une fois
        // La validation individuelle réalisée
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Clacul du premier élément
            if(this.Texte==null && this.Anciennete == null)
            {
                yield return new ValidationResult($"{nameof(Anciennete)} ou {nameof(Texte)} doit être fournie", 
                        new List<string>() { nameof(Anciennete),nameof(Texte) });
            }
            // Le code continue ici si le code appelant cette méthode 
            // décide de lire le deuxieme élément de l'énumarbale

        }
    }
}
