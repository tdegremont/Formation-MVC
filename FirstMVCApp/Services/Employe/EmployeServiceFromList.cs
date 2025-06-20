using FirstMVCApp.Models;
using FirstMVCApp.Services;

namespace FirstMVCApp
{
    public class EmployeServiceFromList : IEmployeService
    {
        private readonly List<Employe> employes;
        private readonly ILogger<EmployeServiceFromList> logger;

        public EmployeServiceFromList(List<Employe> employes, ILogger<EmployeServiceFromList> logger)
        {
            this.employes = employes;
            this.logger = logger;
            logger.LogWarning("Création d'un EmployeServiceFromList");
        }

        public async Task<IEnumerable<Employe>> AugmenterEmployesAsync(EmployeSearchModel search,decimal taux)
        {
            // Pour économiser le processeur (filtrage par foreach ici en dans la vue) => ToList()
            // Pour économiser la mémoire => Laisse en IEnumerable
            var employes = (await GetEmployesAsync(search)).ToList();
            // Itération de modification
            foreach (var e in employes) {
                e.Salaire *= taux;
            }
            return employes;
        }

        public async Task<Employe> DeleteEmployeAsync(string matricule)
        {
            var employe = await GetEmployeAsync(matricule);
   
            employes.Remove(employe);
            return employe;

        }

        public Task<Employe> GetEmployeAsync(string matricule)
        {
            var result= employes.FirstOrDefault(c => c.Matricule == matricule);
            if (result == null) {
                throw new Exception("Matricule non trouvé");
            }
            return Task.FromResult(result);

            //var liste = await GetEmployesAsync(new EmployeSearchModel() { Anciennete = 12 });
            //var DixPremiers = liste.Take(10);

            //// En cas de stateless
            //var DixSuivants = liste.Skip(10).Take(10);

            //// En stateFull
            //var ListePar10 = liste.Chunk(10);
            //foreach(var c in ListePar10)
            //{

            //}

        }

        public string NewMatricule() {
            return (employes
                    .Select(c => int.Parse(c.Matricule))
                    .Max() + 1).ToString().PadLeft(3,'0');
        }

        public Task<IEnumerable<Employe>> GetEmployesAsync(EmployeSearchModel search)
        {
            //throw new EmployeServiceException();
            logger.LogWarning("Recherche d'employés");
            IEnumerable<Employe> result = employes;
            if (search.Texte != null)
            {
                // Where de IEnumerable => Prendre chaque élément de la Liste / Tableau
                //Calculer le bool => Retenir que les élément pour lesquels la condition
                result=result.Where(c=>c.Nom.Contains(search.Texte)
                || c.Prenom.Contains(search.Texte)
                || c.Matricule==search.Texte
                );
            }
            if(search.Anciennete != null)
            {
                result = result.Where(c => c.DateEntree.Year < DateTime.Now.Year - search.Anciennete);
            }
            // Tant que le code appelant n'appelle pas MoveNext du Ienumerable => Aucun code écrit ici n'est exécuté
            return Task.FromResult(result);
        }

        public Task<Employe> AddEmployeAsync(Employe e)
        {
            e.Matricule = NewMatricule();
            e.Nom = e.Nom.ToUpper();
            employes.Add(e);
            return Task.FromResult(e);
        }
    }


}
