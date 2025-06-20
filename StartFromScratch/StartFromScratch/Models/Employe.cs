namespace StartFromScratch.Models
{
    public class Employe
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }

        public string Matricule { get; set; }

        public decimal Salaire { get; set; }

        public DateTime DateEntree { get; set; }
        public bool Actif { get; set; }

    }
}
