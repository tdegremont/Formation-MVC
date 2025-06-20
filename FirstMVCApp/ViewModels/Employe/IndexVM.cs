
using FirstMVCApp.Models;

namespace StartFromScratch.ViewModels.Employe
{
    public class IndexVM
    {
        public IEnumerable<StartFromScratch.Models.Employe> Liste { get; set; }
        public Decimal MasseSalariale { get; set; }

        public EmployeSearchModel SearchModel { get; set; }
    }
}
