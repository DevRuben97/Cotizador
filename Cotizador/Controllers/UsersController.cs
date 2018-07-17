using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cotizador.Models;
using Cotizador.Entitys;

namespace Cotizador.Controllers
{
    public class UsersController : Controller
    {
        CotizadorContext db = new CotizadorContext();
        
       [HttpGet] 
       public ActionResult Login()
        {
            if (Session["IsUserLogin"]!= null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public JsonResult Login(Usuarios User)
        {
            try
            {
                var Usuarios = db.Usuario.ToList().Where(x => x.Usuario.Equals(User.Usuario) && x.Clave.Equals(User.Clave)).First();

                if (Usuarios != null)
                {
                    Session["IsUserLogin"] = true;
                    Session["UserInfo"] = Usuarios;
                    
                    return Json(new { Error = false, Usuario= Usuarios.Nombre }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new {Error = true }, JsonRequestBehavior.AllowGet);
                }
                
            }
            catch(Exception ex)
            {
                return Json(new {ErrorText= ex.Message, Error=true}, JsonRequestBehavior.AllowGet);
            }
        }
        
        public ActionResult Logout()//Cerrar Sesion en el Sistema
        {
            Session["IsUserLogin"] = false;
            Session["UserInfo"] = null;

            ViewBag.Mensaje = "Sesión Cerrada Correctamente";

            return RedirectToAction("Login", "Users");
        }
        [HttpPost]
        public JsonResult GetAllUsers(DataTable Table)
        {
            try
            {

                return Json(new { }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new {Mensaje= ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}