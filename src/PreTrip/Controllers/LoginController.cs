using PreTrip.Model.Classes;
using PreTrip.Utils;
using System.Web.Mvc;
using PreTrip.Services;

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

                return View("Painel", usuario);
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Valida se é um usuário válido.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        private bool ValidarUsuario(Usuario usuario)
        {
            var usuBanco = new UsuarioService().GetWithLoginPass(usuario.Login, usuario.Senha);

            //Se o usuário não existe não pode logar
            if (usuBanco == null)
                return false;

            if (usuBanco.IsAdmin)
                //Admins precisam logar com senha gerada automaticamente
                if (usuario.Senha.ToUpper() != CreatePass.Create().ToUpper())
                    return false;

            return true;
        }
    }
}
