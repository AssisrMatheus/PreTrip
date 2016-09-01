using PreTrip.Model.Classes;
using PreTrip.Services.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarEmpresa(Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                var empresaService = new EmpresasService();
                empresaService.Gravar(empresa);
                Response.Write("<script>alert('Empresa Cadastrada');</script>");

                ModelState.Clear();//sem esse metodo os campos do form nao estavam limpando
            }

            return View("CadastroEmpresa");
        }
    }
}