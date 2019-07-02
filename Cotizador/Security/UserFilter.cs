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

        public string Role { get; set; } //Rol del Usuario

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["IsUserLogin"] == null || (bool)HttpContext.Current.Session["IsUserLogin"]==false)
            {
                //Obtener el usuario actual:
                Models.Usuarios user =(Models.Usuarios) HttpContext.Current.Session["User"];
                if (user.Rol.Equals(Role) == false)
                {
                    filterContext.Result = new RedirectResult("/Security/InvalidAccess");
                }
                else
                {
                    filterContext.Result = new RedirectResult("/Security/Login");
                }
                
            }
           
            base.OnActionExecuting(filterContext);
            
        }
    }
}