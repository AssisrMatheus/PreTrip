using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using PreTrip.Services.Autenticacao;
using PreTrip.Services.Usuarios;
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
                ViewBag.QuantidadeViagens = db.Viagem.Count();
            }

            return View();
        }

        /// <summary>
        /// Ação de fazer a autenticação de usuário
        /// </summary>
        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            if(ModelState.IsValid)
            {
                var service = new AutenticacaoService();
                var autenticado = service.Autenticar(usuario.Login, usuario.Senha);

                if (autenticado)
                {
                    if ((Session["Usuario"] as Usuario).IsAdmin)
                        return RedirectToAction("Administrativo", "Administrativo");
                    else
                        return RedirectToAction("PaginalPrincipalUsuario", "Usuario", usuario);
                }
            }

            return View("Index");
        }

        /// <summary>
        /// Página de cadastro.
        /// </summary>
        public ActionResult Cadastrar()
        {
            return View();
        }

        /// <summary>
        /// Ação de cadastrar no banco
        /// </summary>
        [HttpPost]
        public ActionResult Cadastrar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var service = new UsuariosService();
                service.Inserir(usuario);

                return View("Index", usuario);
            }
            else
            {
                //Guardamos os erros
                var statesErrors = ModelState.Values.Select(x => x.Errors).Where(x => x.Any());
                ViewBag.Errors = statesErrors.Select(x => x.Select(y => y.ErrorMessage));

                return View(usuario);
            }
        }
    }
}