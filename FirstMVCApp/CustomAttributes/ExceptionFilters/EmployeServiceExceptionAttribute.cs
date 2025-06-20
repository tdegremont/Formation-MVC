using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace FirstMVCApp.CustomAttributes.ExceptionFilters

{
    public class EmployeServiceExceptionAttribute : TypeFilterAttribute
    {
        public EmployeServiceExceptionAttribute() : base(typeof(EmployeServiceExceptionFilter))
        {

        }
    }
}
