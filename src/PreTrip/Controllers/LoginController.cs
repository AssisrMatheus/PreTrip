using PreTrip.Model.Classes;
using PreTrip.Utils;
using System.Web.Mvc;
using PreTrip.Services;

namespace PreTrip.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index(Usuario usuario)
        {
            return View("Administrativo", usuario);
        }

        [HttpPost]
        public ActionResult Logar(Usuario usuario)
        {            
            //Se é um usuário real
            if (ValidarUsuario(ref usuario))
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
        private bool ValidarUsuario(ref Usuario usuario)
        {
            // Busca o usuário no banco
            var usuarioBanco = new UsuarioService().GetWithLoginPass(usuario.Login, usuario.Senha);

            // Busca o usuário administrador caso tenha sido digitado a senha (Token) de administrador corretamente.
            if (usuarioBanco == null && usuario.Senha.ToUpper() == CreatePass.Create().ToUpper())
                usuarioBanco = new UsuarioService().GetWithLoginPassAdmin(usuario.Login);

            // Se o usuário não existe não pode logar
            if (usuarioBanco == null)
                return false;
            // Essa verificação foi necessária pois se alguém digitar o usuário e senha 
            // de quando o administrador foi cadastrado, é retornado falso.
            // É preciso que a senha do administrador seja igual a senha gerada no dia.
            if (usuarioBanco.IsAdmin)
                // Admins precisam logar com senha (Token) gerada automaticamente no dia.
                if (usuario.Senha.ToUpper() != CreatePass.Create().ToUpper())
                    return false;

            usuario = usuarioBanco;
            return true;
        }
    }
}
