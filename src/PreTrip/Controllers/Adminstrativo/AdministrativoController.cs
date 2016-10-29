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

namespace PreTrip.Controllers
{
    public class AdministrativoController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Usuarios = new UsuariosService().GetUsers();
            //return View();
            
            //Por enquanto não teremos financeiro(inicial de administrativo)
            //Então retornamos para viagens
            return this.Viagens();
        }

        public ActionResult Usuarios()
        {
            ViewBag.Usuarios = new UsuariosService().GetUsers();

            return View();
        }

        public ActionResult Viagens()
        {
            ViewBag.Viagens = new ViagensService().GetViagens();

            return View();
        }

        [HttpPost]
        public ActionResult CadastrarUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var service = new UsuariosService();

                //Isso não pode estar no construtor pois é uma navigation property(virtual)
                usuario.Pessoa.ContaBancaria = new ContaBancaria();

                service.Inserir(usuario);
            }
            
            return View("Index");
        }

        [HttpPost]
        public ActionResult CadastrarEmpresa(Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                var empresaService = new EmpresasService();
                empresaService.Inserir(empresa);
            }

            return View("Index");
        }
    }
}