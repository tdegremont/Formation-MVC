
using FirstMVCApp.Models;
using StartFromScratch.Models;

namespace FirstMVCApp.Services
{
    public interface IEmployeService
    {
        Task<IEnumerable<Employe>> GetEmployesAsync(EmployeSearchModel search);
        Task<Employe> GetEmployeAsync(string matricule);

        Task<IEnumerable<Employe>> AugmenterEmployesAsync(EmployeSearchModel search);

    }
}
