using PreTrip.Model.Classes;
using PreTrip.Utils;
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
            //Se é um usuário real
            if (LoginUtils.ValidarUsuario(ref usuario))
            {
                if (usuario.IsAdmin)
                    return RedirectToAction("Administrativo", "Administrativo", usuario);

                // Fazer o redirecionamento para o controller de usuário passando a action necessária.
                return RedirectToAction("Painel", "Usuario", usuario);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
