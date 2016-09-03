using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PreTrip.Model.Classes;
using PreTrip.Services.Empresas;

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
        
        public ActionResult AlterarUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AlterarUsuario(Usuario Usuario)
        {
            return RedirectToAction("Index", "Usuario");
        }

        public ActionResult Empresas()
        {

#warning Aqui o usuário está podendo ver TODAS as empresas. Não podemos deixar ele cadastrar empresa somente, ele precisa cadastrar uma viagem, para poder fazer o join de Viagens que ele tem pra poder pegar as empresas e poder listar
            ViewBag.Empresas = new EmpresasService().GetAll();

            return View();
        }

        public ActionResult CadastrarEmpresa()
        {         
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarEmpresa(Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                var empresaService = new EmpresasService();
                empresaService.Inserir(empresa);
                ModelState.Clear();
            }
            //sem o redirect os dados do formulário nao estavam limpando.
            return RedirectToAction("Empresas");
        }
    }
}