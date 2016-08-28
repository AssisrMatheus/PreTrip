using PreTrip.Model.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PreTrip.Controllers
{
    public class LoginController : Controller
    {
        
        public ActionResult Index()
        {
            return View("Administrativo", new Usuario());
        }

        [HttpPost]
        public ActionResult Logar(Usuario usuario)
        {
            return View("Administrativo",new Usuario());
        }

        private ActionResult ValidarUsuario(Usuario usuario)
        {
            if (usuario.IsAdmin)
            {
                return View("Login/Administrativo", usuario);
            }

            return RedirectToAction("Index","Home", usuario);
        }
    }
}
