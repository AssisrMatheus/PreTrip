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
            if (ModelState.IsValid && CadastroUtils.VerificaDadosUsuario(usuario))
            {
                CadastroUtils.Gravar(usuario);

                return RedirectToAction("Index", "Home", usuario);
            }
            
            var statesErrors = ModelState.Values.Select(x => x.Errors).Where(x => x.Any());
            ViewBag.Errors = statesErrors.Select(x => x.Select(y => y.ErrorMessage));

            return RedirectToAction("Cadastro", "Home", usuario);
        }
    }
}