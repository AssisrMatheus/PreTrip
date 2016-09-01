using PreTrip.Model.Classes;
using PreTrip.Services.Empresas;
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

        [HttpPost]
        public ActionResult CadastrarEmpresa(Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                var empresaService = new EmpresasService();
                empresaService.Gravar(empresa);
                Response.Write("<script>alert('Empresa inserida!!') </script>");
                ModelState.Clear();
            }

            return View("CadastroEmpresa");
        }

        public ActionResult VisualizarEmpresas()
        {
            ViewBag.Empresas = new EmpresasService().GetAll();
            return View();
        }
    }
}