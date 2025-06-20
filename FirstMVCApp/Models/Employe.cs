using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StartFromScratch.Models
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
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        
        public DateTime DateEntree { get; set; }

        [DisplayName("En activité")]
        public bool Actif { get; set; }

    }
}
