using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PreTrip.Model.Classes;

namespace PreTrip.Controllers
{
    public class CadastroController : Controller
    {
        [HttpPost]
        public ActionResult Cadastrar(Usuario user)
        {
            if (this.VerificaDadosUsuario(user))
            {
#warning Adicionar os dados no banco.
            }

#warning Verificar como retorna essa view.
            return View("Home/Index.cshtml", new Usuario());
        }

        /// <summary>
        /// Verifica se os dados do usuário foram preenchidos corretamente no cadastro.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool VerificaDadosUsuario(Usuario user)
        {
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