
using FirstMVCApp.Models;
using FirstMVCApp.Services;

namespace FirstMVCApp.ViewModels.Employe
{
    public class IndexVM
    {
        public IEnumerable<FirstMVCApp.Models.Employe> Liste { get; set; }
        public Decimal MasseSalariale { get; set; }

        public EmployeSearchModel SearchModel { get; set; }
    }
}
