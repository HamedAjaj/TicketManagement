using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TicketManagement.Filters
{
    public class HandleErrorAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
           ContentResult result= new ContentResult();
            result.Content = "Sorry try again";
            context.Result = result;
        }
    }
}
