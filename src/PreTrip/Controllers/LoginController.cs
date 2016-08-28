using PreTrip.Model.Classes;
using System.Web.Mvc;
using PreTrip.Services;

namespace PreTrip.Controllers
{
    /// <summary>
    /// Controller para definir se vai ir para uma página administrativa ou de usuário comum
    /// </summary>
    public class LoginController : Controller
    {
        [HttpPost]
        public ActionResult Logar(Usuario usuario)
        {
            var service = new UsuarioService();

            var usuBanco = service.GetWithLoginPass(usuario.Login, usuario.Senha);

            //Se é um usuário real
            if (usuBanco != null)
            {
                if (usuBanco.IsAdmin)
                    return RedirectToAction("Administrativo", "Administrativo", usuario);

                // Fazer o redirecionamento para o controller de usuário passando a action necessária.
                return RedirectToAction("Painel", "Usuario", usuario);
            }

            //Se veio nulo o usuário não existe, voltar para index
            return RedirectToAction("Index", "Home");
        }
    }
}
