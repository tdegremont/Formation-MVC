
using FirstMVCApp.Models;

namespace FirstMVCApp
{
    public interface IEmployeService
    {

        Task<Employe> AddEmployeAsync(Employe e);

        Task<IEnumerable<Employe>> GetEmployesAsync(EmployeSearchModel search);
        Task<Employe> GetEmployeAsync(string matricule);

        Task<Employe> DeleteEmployeAsync(string matricule);

        Task<IEnumerable<Employe>> AugmenterEmployesAsync(EmployeSearchModel search,decimal taux);
    }
}
