using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using PreTrip.Model.Services;
using PreTrip.Utils;
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
            //Se é um usuário real
            if (ValidarUsuario(usuario))
            {
                if (usuario.IsAdmin)
                    return View("Administrativo", usuario);
                else
                    return View("Painel", usuario);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private bool ValidarUsuario(Usuario usuario)
        {
            var usuBanco = new UsuarioService().GetWithLoginPass(usuario.Login, usuario.Senha);

            //Se o usuário não existe não pode logar
            if (usuBanco == null)
            {
                return false;
            }

            if (usuBanco.IsAdmin)
            {
                //Admins precisam logar com senha gerada automaticamente
                if (usuario.Senha.ToUpper() != CreatePass.Create().ToUpper())
                    return false;
            }

            return true;
        }
    }
}
