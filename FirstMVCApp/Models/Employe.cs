using FirstMVCApp.CustomsAttributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FirstMVCApp.Models
{
    public class Employe
    {

        

        [DisplayName("Nom")]
        public string Nom { get; set; }


        [DisplayName("Prénom")]

        public string Prenom { get; set; }


        [DisplayName("Matricule")]

        public string Matricule { get; set; }

        [DisplayName("Salaire")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Salaire { get; set; }

        [DisplayName("Date d'entrée")]
        [JourdeSemaine(true, "{0} doit être un jour ouvré samedi {1}")]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        
        public DateTime DateEntree { get; set; }=DateTime.Now;

        [DisplayName("En activité")]
        public bool Actif { get; set; } = true;

    }
}
