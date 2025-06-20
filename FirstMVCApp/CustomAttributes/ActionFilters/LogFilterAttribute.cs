using Microsoft.AspNetCore.Mvc;

namespace FirstMVCApp.CustomAttributes.ActionFilters
{
    public class LogFilterAttribute : TypeFilterAttribute<LogFilter>
    {
        public LogFilterAttribute(string messageBefore, string messageAfter)
        {
            this.Arguments=new object[] {messageBefore,messageAfter}; 
        }
    }
}
