using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PreTrip.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {            
            using(var db = new PreTripDB())
            {
                ViewBag.QuantidadeViagens = 0;
                //db.Viagem.Count();
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Action chamada ao clicar no botão cadastre-se no topo.
        /// </summary>
        /// <returns></returns>
        public ActionResult Cadastro()
        {
            return View();
        }

        /// <summary>
        /// Caso o usuário preencha na tela inicial os campos 'Usuario' e 'Senha'
        /// Ele já direciona para a view de cadastro preenchendo os campos de usuário e senha.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Cadastro(Usuario usuario)
        {
            return View(usuario);
        }
    }
}