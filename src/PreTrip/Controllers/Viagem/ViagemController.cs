using PreTrip.Model.Classes;
using PreTrip.Services.Viagens;
using PreTrip.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PreTrip.Controllers
{
    public class ViagemController : Controller
    {
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


        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(Viagem viagem)
        {
            if(ModelState.IsValid)
            {
                new ViagensService().Inserir(viagem);
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}