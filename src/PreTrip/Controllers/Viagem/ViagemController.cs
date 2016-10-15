﻿using PreTrip.Attributes;
using PreTrip.Model.Classes;
using PreTrip.Services.Enderecos;
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

        [UsuarioLogado]
        public ActionResult Cadastrar()
        {
            var viewModel = new ViagensViewModel();
            viewModel.Viagem = new Viagem();

            return View(viewModel);
        }

        [HttpPost]
        [UsuarioLogado]
        public ActionResult Cadastrar(ViagensViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                viewModel.Viagem.Empresa.Usuario = PreTripSession.Usuario;
                viewModel.Viagem.Pessoa = PreTripSession.Usuario.Pessoa;

                new ViagensService().Gravar(viewModel.Viagem);
                return RedirectToAction("Visualizar", "Viagem", new { id = viewModel.Viagem.Id });
            }

            return View(viewModel);
        }

        [HttpPost]
        [UsuarioLogado]
        public ActionResult Avaliar(ViagensViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var service = new ViagensService();
                viewModel.Avaliacao.Usuario = PreTripSession.Usuario;

                service.InserirAvaliacao(viewModel.Avaliacao);
            }

            return RedirectToAction("Visualizar", new RouteValueDictionary(new { id = viewModel.Viagem.Id }));
        }

        [HttpPost]
        public ActionResult Buscar(ViagensViewModel v)
        {
            v.Viagens = new ViagensService().GetAllFilter(v.BuscaViagens);
            SalvarHistoricoBusca(v);
            v.HeaderViagens = "Viagens Encontradas";
            return View("Index", v);
        }

        public void SalvarHistoricoBusca(ViagensViewModel viagensVM)
        {
            if (PreTripSession.Usuario != null)
            {
                var viagensService = new ViagensService();
                viagensVM.BuscaViagens.Usuario = PreTripSession.Usuario;
                viagensService.InserirBusca(viagensVM.BuscaViagens);
            }
        }
    }
}