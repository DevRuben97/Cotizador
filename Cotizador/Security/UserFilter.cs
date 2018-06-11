using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cotizador.Security
{
    public class UserFilter: ActionFilterAttribute, IActionFilter
    {
        //Variables Globales de la clase:

        public string User { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["IsUserLogin"] == null || (bool)HttpContext.Current.Session["IsUserLogin"]==false)
            {
                filterContext.Result = new RedirectResult("/Users/Login");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}