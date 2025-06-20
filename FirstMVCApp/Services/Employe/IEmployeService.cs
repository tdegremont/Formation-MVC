
using FirstMVCApp.Models;

namespace FirstMVCApp.Services
{
    public interface IEmployeService
    {
        Task<IEnumerable<Employe>> GetEmployesAsync(EmployeSearchModel search);
        Task<Employe> GetEmployeAsync(string matricule);

        Task<Employe> DeleteEmployeAsync(string matricule);

        Task<Employe> CreateEmployeAsync(Employe employe); 

        Task<IEnumerable<Employe>> AugmenterEmployesAsync(EmployeSearchModel search, decimal taux);
    }
}
