using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FirstMVCApp.CustomAttributes.ActionFilters
{
    /// <summary>
    /// Classe de filtre destinée à intercepter avant / après l'action
    /// </summary>
    public class LogFilter : IAsyncActionFilter
    {

        private readonly ILogger<LogFilter> logger;

        public LogFilter(string messageBefore,
                            string messageAfter, ILogger<LogFilter> logger)
        {
            MessageBefore = messageBefore;
            MessageAfter = messageAfter;
            this.logger = logger;
        }

        public string MessageBefore { get; }
        public string MessageAfter { get; }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            this.logger.LogWarning(MessageBefore,
                    context.HttpContext.GetRouteValue("Controller"),
                    context.HttpContext.GetRouteValue("Action")
                    );
            // L'exécution de cette méthode entraine l'exécution de l'action
            await next();
            this.logger.LogWarning(MessageAfter, context.HttpContext.GetRouteValue("Controller"),
                    context.HttpContext.GetRouteValue("Action"));

        }
    }
}
