using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PreTrip.Model.Classes;

namespace PreTrip.Controllers
{
    /// <summary>
    /// Controller das views do usuário comum
    /// </summary>
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CadastroEmpresa()
        {
            return View("CadastroEmpresa");
        }

        public ActionResult AlterarUsuario()
        {
            return View();
        }
    }
}