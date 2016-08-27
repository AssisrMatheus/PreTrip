using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using System.Security.Cryptography;
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
            else
            {
                var statesErrors = ModelState.Values.Select(x => x.Errors).Where(x => x.Any());
                ViewBag.Errors = statesErrors.Select(x => x.Select(y => y.ErrorMessage));
                return RedirectToAction("Cadastro", "Home", usuario);
            }
        }

        /// <summary>
        /// Verifica se os dados do usuário foram preenchidos corretamente no cadastro.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool VerificaDadosUsuario(Usuario user)
        {
            if (!ModelState.IsValid)
                return false;

            if (string.IsNullOrEmpty(user.Login))
                return false;

            if (string.IsNullOrEmpty(user.Senha))
                return false;

            if(string.IsNullOrEmpty(user.Pessoa.Nome))
                return false;

            return true;
        }
    }
}