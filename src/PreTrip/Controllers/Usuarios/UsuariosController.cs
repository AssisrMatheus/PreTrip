using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PreTrip.Controllers.Usuarios
{
    /// <summary>
    /// Controller das views do usuário comum
    /// </summary>
    public class UsuariosController : Controller
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
    }
}