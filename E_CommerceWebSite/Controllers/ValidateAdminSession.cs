using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace E_CommerceWebSite.Controllers
{
    internal class ValidateAdminSession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {

                if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["AdminUser"])))
                {
                    filterContext.Controller.TempData["ErrorMessage"] = "Session has been expired please Login";
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Member", action = "Login" }));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}