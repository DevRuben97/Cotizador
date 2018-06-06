using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cotizador.Controllers
{
    public class UsersController : Controller
    {
       [HttpGet] 
       public ViewResult Login()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Login(string usuario, string clave, bool remenber)
        {
            try
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}