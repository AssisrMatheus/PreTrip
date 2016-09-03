using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using PreTrip.Services.Autenticacao;
using PreTrip.Services.Usuarios;
using PreTrip.Session;
using PreTrip.ViewModel;
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
            using (var db = new PreTripDB())
            {
                ViewBag.QuantidadeViagens = db.Viagens.Count();
            }

            return View();
        }

        /// <summary>
        /// Ação de fazer a autenticação de usuário
        /// </summary>
        [HttpPost]
        public ActionResult Login(HomeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var usuario = viewModel.Usuario;
                var service = new AutenticacaoService();
                var autenticado = service.Autenticar(usuario.Login, usuario.Senha);

                if (autenticado)
                {
                    if (PreTripSession.Usuario.IsAdmin)
                        return RedirectToAction("Index", "Administrativo");
                    else
                        // Fazer o redirecionamento para o controller de usuário passando a action necessária.
                        return RedirectToAction("Index", "Usuario");
                }
                else
                {
                    ModelState.AddModelError("Login", "Login ou senha inválidos!");
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
            var viewModel = new HomeViewModel()
            {
                Usuario = usuario
            };

            if (ModelState.IsValid)
            {
                var service = new UsuariosService();
                service.Inserir(usuario);

                return View("Index", viewModel);
            }
            else
            {
                //Guardamos os erros
                var statesErrors = ModelState.Values.Select(x => x.Errors).Where(x => x.Any());
                ViewBag.Errors = statesErrors.Select(x => x.Select(y => y.ErrorMessage));

                return View(viewModel);
            }
        }
    }
}