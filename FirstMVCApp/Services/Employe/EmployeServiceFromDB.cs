using AutoMapper;
using FirstMVCApp.Models;
using FirstMVCApp.Services.DALEmployes;
using Microsoft.EntityFrameworkCore;

namespace FirstMVCApp.Services
{
    public class EmployeServiceFromDB : IEmployeService
    {
        private readonly EmployeDbContext context;
        private readonly IMapper mapper;

        public EmployeServiceFromDB(EmployeDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Task<Models.Employe> AddEmployeAsync(Models.Employe e)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Models.Employe>> AugmenterEmployesAsync(EmployeSearchModel search, decimal taux)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Employe> DeleteEmployeAsync(string matricule)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Employe> GetEmployeAsync(string matricule)
        {
            EmployeDAO? employeado = context.Employes.FirstOrDefault(c => c.Matricule == matricule);
            if (employeado == null)
            {
                return Task.FromResult<Models.Employe>(null);
            }
            else
            {
                // Mapper l'objet DAO vers l'objet Employe
                return Task.FromResult(mapper.Map<Models.Employe>(employeado));
            }
        }

        public Task<IEnumerable<Models.Employe>> GetEmployesAsync(EmployeSearchModel search)
        {
            // SELECT * FROM tbl_Employes
            IQueryable<EmployeDAO> query = context.Employes;
            var requete = query.ToQueryString();

            if (search.Texte != null)
            {
                // IQueryable => Ajoute une clause Where a query
                // SELECT * FROM tbl_Employes WHERE ...
                query = query.Where(c => c.Name.Contains(search.Texte)
                || c.Prenom.Contains(search.Texte)
                || c.Matricule.Contains(search.Texte)
                );
            }
            requete = query.ToQueryString();
            if (search.Anciennete != null)
            {
                // IQueryable => Ajoute une clause Where a query
                // SELECT * FROM tbl_Employes WHERE ...
                query = query.Where(c => c.DateEntree.Year < DateTime.Now.Year - search.Anciennete);
            }
            requete = query.ToQueryString();


            // Quelle est la requète envoyée
            // Aucune

            // Au moment de l'utilisation la requète sera envoyé
            //foreach(var e in pocos)
            //{

            //}
            return Task.FromResult(mapper.Map<IEnumerable<Employe>>(query));
            

        }
    }
}
