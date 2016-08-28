using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using PreTrip.Services;
using PreTrip.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PreTrip.Controllers
{
    public class AdministrativoController : Controller
    {
        // GET: Administrador
#warning Precisa ser alterado a view pois esta sendo referenciada no Login, precisa sair do login e passar para o controller Administrativo
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarUsuario(Usuario usuario)
        {
            if (VerificaDadosUsuario(usuario))
            {
                this.Gravar(usuario);
#warning Testar para ver se está passando o usuario certo ou se precisa ser o usuario administrador.
                return RedirectToAction("Index", "Login", null);
            }

            var statesErrors = ModelState.Values.Select(x => x.Errors).Where(x => x.Any());
            ViewBag.Errors = statesErrors.Select(x => x.Select(y => y.ErrorMessage));

#warning Testar para ver se está passando o usuario certo ou se precisa ser o usuario administrador.
            return RedirectToAction("Index", "Login", usuario);
        }

        private void Gravar(Usuario usuario)
        {
            using (var db = new PreTripDB())
            {
                //Converte para md5
                usuario.Senha = CreateMD5.GetHash(usuario.Senha);

                db.Usuario.Add(usuario);
                db.SaveChanges();
            }
        }

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