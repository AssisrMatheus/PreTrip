using PreTrip.Model.Classes;
using PreTrip.Services.Viagens;
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
            return View();
        }

        [HttpPost]
        //POR ENQUANTO USUARIO SÓ ESTÁ COMO UM PLACEHOLDER, SERÁ SUBSTITUÍDO PARA "BUSCA"
        //a BUSCA É FEITA QUANDO PASSADA UM OBJETO PARA O POST
        public ActionResult Index(Usuario usuario)
        {
            return View();
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
                return View("Index");
            }

            return View();
        }
    }
}