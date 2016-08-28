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
            // Busca o usuário no banco
            usuario = new UsuarioService().GetWithLoginPass(usuario.Login, usuario.Senha);

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
            //Se o usuário não existe não pode logar
            if (usuario == null)
                return false;

            if (usuario.IsAdmin)
                //Admins precisam logar com senha (Token) gerada automaticamente no dia.
                if (usuario.Senha.ToUpper() != CreatePass.Create().ToUpper())
                    return false;

            return true;
        }
    }
}
