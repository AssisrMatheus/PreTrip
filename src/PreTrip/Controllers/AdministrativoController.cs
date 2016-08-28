using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using PreTrip.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PreTrip.Controllers
{
    public class AdministrativoController : Controller
    {
        // GET: Administrador
        public ActionResult Administrativo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarUsuario(Usuario usuario)
        {
            var service = new UsuarioService();

            if (ModelState.IsValid)
            {
                service.Gravar(usuario);
            }
            else
            {
                var statesErrors = ModelState.Values.Select(x => x.Errors).Where(x => x.Any());
                ViewBag.Errors = statesErrors.Select(x => x.Select(y => y.ErrorMessage));
            }
            
            return RedirectToAction("Administrativo", "Administrativo");
        }
    }
}