using PreTrip.Model.Classes;
using PreTrip.Services.Viagens;
using PreTrip.Session;
using PreTrip.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PreTrip.Controllers
{
    public class ViagemController : Controller
    {
        [HttpPost]
        public ActionResult Buscar(ViagensViewModel v)
        {
            
            v.Viagens = new ViagensService().GetAllFilter(v.BuscaViagens);
            v.HeaderViagens = "Viagens Encontradas";
            return View("Index",v);
        }
        // GET: Viagem
        public ActionResult Index()
        {
            var viewModel = new ViagensViewModel();

            viewModel.Viagens = new ViagensService().GetAll();
            viewModel.HeaderViagens = "Últimas viagens";

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(ViagensViewModel viewModel)
        {
            viewModel.Viagens = new ViagensService().GetAll();
            viewModel.HeaderViagens = "Resultados da pesquisa";

            return View(viewModel);
        }

        public ActionResult Visualizar(int id)
        {
            var viewModel = new ViagensViewModel();

            viewModel.Viagem = new ViagensService().GetViagem(id);

            return View(viewModel);
        }

        public ActionResult Cadastrar()
        {
            var viewModel = new ViagensViewModel();
            viewModel.Viagem = new Viagem();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Cadastrar(ViagensViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                new ViagensService().Inserir(viewModel.Viagem);
                return RedirectToAction("Index","Usuario");
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Avaliar(ViagensViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var service = new ViagensService();
                viewModel.Avaliacao.ViagemId = viewModel.Viagem.Id;
                viewModel.Avaliacao.Usuario = PreTripSession.Usuario;

                service.InserirAvaliacao(viewModel.Avaliacao);
            }

            return RedirectToAction("Visualizar", new RouteValueDictionary(new { id = viewModel.Viagem.Id }));
        }
    }
}