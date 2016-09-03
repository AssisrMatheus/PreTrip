using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PreTrip.Model.Classes;
using PreTrip.Services.Empresas;
using PreTrip.Services.Usuarios;

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

        [HttpPost]
        public ActionResult AlterarUsuario(Usuario usuario, string oldSenha)
        {
#warning Falta terminar muita coisa aqui ainda.
            if (string.IsNullOrEmpty(usuario.Senha))
                usuario.Senha = oldSenha;

            var serviceUsuario = new UsuariosService();
            serviceUsuario.Alterar(usuario);

            return RedirectToAction("Index", "Usuario");
        }

        [HttpPost]
         public ActionResult CadastrarEmpresa(Empresa empresa)
         {
             if (ModelState.IsValid)
             {
                 var empresaService = new EmpresasService();
                 empresaService.Gravar(empresa);
                ModelState.Clear();
             }
             //sem o redirect os dados do formulário nao estavam limpando.
             return CadastroEmpresa();
         }
    }
}