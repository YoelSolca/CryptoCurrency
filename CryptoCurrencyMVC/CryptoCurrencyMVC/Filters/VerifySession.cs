using CryptoCurrencyMVC.Models;
using System.Web.Http.Filters;

namespace CryptoCurrencyMVC.Filters
{
    public class VerifySession : ActionFilterAttribute 
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}
