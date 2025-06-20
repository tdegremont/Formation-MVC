using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_tests
{
    internal class Personne
    {
        public Personne(string nom)
        {
            Nom = nom;
        }

        public string Nom { get; set; }
    }

    class Travailleur : Personne
    {
        public Travailleur(string nom ) : base(nom)
        {
            
        }

    }




}
