using System.Linq;
using System.Web.Mvc;
using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using PreTrip.Services;
using PreTrip.Utils;

namespace PreTrip.Controllers
{
    public class CadastroController : Controller
    {
        [HttpPost]
        public ActionResult Cadastrar(Usuario usuario)
        {
            if (this.VerificaDadosUsuario(usuario))
            {
                using (var db = new PreTripDB())
                {
                    //Converte para md5
                    usuario.Senha = CreateMD5.GetHash(usuario.Senha);

                    db.Usuario.Add(usuario);
                    db.SaveChanges();
                }

                return RedirectToAction("Index", "Home", usuario);
            }
            
            var statesErrors = ModelState.Values.Select(x => x.Errors).Where(x => x.Any());
            ViewBag.Errors = statesErrors.Select(x => x.Select(y => y.ErrorMessage));

            return RedirectToAction("Cadastro", "Home", usuario);
        }

        /// <summary>
        /// Verifica se os dados do usuário foram preenchidos corretamente no cadastro.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        private bool VerificaDadosUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
                return false;

            if (string.IsNullOrEmpty(usuario.Login))
                return false;

            if (string.IsNullOrEmpty(usuario.Senha))
                return false;

            if (string.IsNullOrEmpty(usuario.Pessoa.Nome))
                return false;

            var usuBanco = new UsuarioService().GetWithLoginPass(usuario.Login, usuario.Senha);

            //Se o usuário existir não podemos deixar registrar
            if (usuBanco != null)
                return false;

            return true;
        }
    }
}