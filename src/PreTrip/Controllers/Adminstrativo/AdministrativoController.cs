using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using PreTrip.Services.Empresas;
using PreTrip.Services.Usuarios;
using PreTrip.Services.Viagens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PreTrip.Controllers.Administrativo
{
    public class AdministrativoController : Controller
    {
        // GET: Administrador
        public ActionResult Administrativo()
        {
            ViewBag.Usuarios = new UsuariosService().GetAll();
            //return View();

            //Por enquanto não teremos financeiro(inicial de administrativo)
            //Então retornamos para viagens
            return this.Viagens();
        }

        public ActionResult Usuarios()
        {
            ViewBag.Usuarios = new UsuariosService().GetAll();

            return View();
        }

        public ActionResult Viagens()
        {
            ViewBag.Viagens = new ViagensService().GetAll();

            return View();
        }

        [HttpPost]
        public ActionResult CadastrarUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var service = new UsuariosService();
                service.Gravar(usuario);
            }
            else
            {
                var statesErrors = ModelState.Values.Select(x => x.Errors).Where(x => x.Any());
                ViewBag.Errors = statesErrors.Select(x => x.Select(y => y.ErrorMessage));
            }
            
            return RedirectToAction("Administrativo", "Administrativo");
        }

        public ActionResult CadastrarEmpresa(Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                var empresaService = new EmpresasService();
                empresaService.Gravar(empresa);
            }

            return RedirectToAction("Administrativo", "Administrativo");
        }
    }
}