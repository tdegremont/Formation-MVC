using FirstMVCApp.CustomAttributes.ValidationAttributes;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FirstMVCApp.Services.DALEmployes
{
    /// <summary>
    /// Cette classe représente la structure de la table dans la BDD
    /// Différence avec Employe
    /// Employe : Dans mon appli, les données que je manipule
    /// Différence : Dans le DAO on peut trouver plein d'informations qui ne sont pas pertinentes dans notre appli
    /// /Eviter de les confondre
    /// // DAO => Données sensibles (tel perso,salaire,mdp, Données techniques
    /// </summary>
    public class EmployeDAO
    {
        // Id de type GUID
        // Peut être généré sans connaitre les données déjà présentes dans la BDD
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Prenom { get; set; }
        public string Matricule { get; set; }
        public decimal Salaire { get; set; }

        public DateTime DateEntree { get; set; }
        public bool Actif { get; set; }

        public DateTime DerniereModif { get; set; }
        public string  Confidentiel { get; set; }
    }
}
