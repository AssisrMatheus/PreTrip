using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PreTrip.Model.Classes;
using PreTrip.Model.Context;

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
                    db.Usuario.Add(usuario);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index", "Home", usuario);
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

            if (user.Pessoa.Cpf == 0 || user.Pessoa.Cpf.ToString().Length != 11)
                return false;

            if (user.Pessoa.DtNascimento == default(DateTime))
                return false;

            return true;
        }
    }
}