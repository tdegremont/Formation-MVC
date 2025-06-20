using FirstMVCApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FirstMVCApp.CustomAttributes.ExceptionFilters
{
    /// <summary>
    /// Classe de filtre destinée à gérer les erreurs de type EmployeServiceException
    /// </summary>
    public class EmployeServiceExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<EmployeServiceExceptionFilter> logger;
        private readonly IConfiguration config;

        public EmployeServiceExceptionFilter(

            ILogger<EmployeServiceExceptionFilter> logger,
            IConfiguration config
            )
        {
            this.logger = logger;
            this.config = config;
        }
        public void OnException(ExceptionContext context)
        {

            if (context.Exception is EmployeServiceException)
            {
                logger.LogWarning("Exception du service");
                context.Result = new ViewResult() { ViewName = config.GetSection("Services:Employe:ErrorPage").Value};
            }
        }
    }
}
