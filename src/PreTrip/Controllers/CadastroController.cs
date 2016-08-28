using System.Linq;
using System.Web.Mvc;
using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using PreTrip.Services;

namespace PreTrip.Controllers
{
    public class CadastroController : Controller
    {
        [HttpPost]
        public ActionResult Cadastrar(Usuario usuario)
        {
            var service = new UsuarioService();

            if (ModelState.IsValid)
            {
                service.Gravar(usuario);

                return RedirectToAction("Index", "Home", usuario);
            }
            
            //Guardamos os erros
            var statesErrors = ModelState.Values.Select(x => x.Errors).Where(x => x.Any());
            ViewBag.Errors = statesErrors.Select(x => x.Select(y => y.ErrorMessage));

            return RedirectToAction("Cadastro", "Home", usuario);
        }
    }
}