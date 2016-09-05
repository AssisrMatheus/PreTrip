using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PreTrip.Model.Classes;
using PreTrip.Services.Empresas;
using PreTrip.Services.Usuarios;
using PreTrip.Session;

namespace PreTrip.Controllers
{
    /// <summary>
    /// Controller das views do usuário comum
    /// </summary>
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult AlterarUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AlterarUsuario(Usuario usuario, string oldSenha)
        {
#warning Falta terminar muita coisa aqui ainda.
            if (string.IsNullOrEmpty(usuario.Senha))
                usuario.Senha = oldSenha;

            var serviceUsuario = new UsuariosService();
            serviceUsuario.Alterar(usuario);

            return RedirectToAction("Index", "Usuario");
        }

        public ActionResult Empresas()
        {

#warning Aqui o usuário está podendo ver TODAS as empresas. Não podemos deixar ele cadastrar empresa sozinha, ele precisa cadastrar uma viagem com empresa, para poder fazer o join de Viagens que ele tem pra poder pegar as empresas e poder listar
            ViewBag.Empresas = new EmpresasService().GetAll();

            return View();
        }

        public ActionResult CadastrarEmpresa()
        {         
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarEmpresa(Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                var empresaService = new EmpresasService();
                empresaService.Inserir(empresa);
                ModelState.Clear();
            }

            //Sem o redirect os dados do formulário nao estavam limpando.
            return RedirectToAction("Empresas");
        }

        public ActionResult Interesses()
        {
#warning Terminar esse método. Precisa passar por (~~~~viewbag~~~~NÃO USE VIEWBAG, USE VIEWMODEL). a lista de interesses do usuario e uma lista com todas as cidades já cadastradas
            var listaInteresses = new UsuariosService().GetUsuarioInteresses();
            return View();
        }

        [HttpPost]
        public ActionResult Interesses(Usuario usuario, IEnumerable<Interesse> listaInteresses)
        {
#warning Pesquisar como recebe uma lista da view.
            return RedirectToAction("Index", "Usuario");
        }

        public ActionResult HistoricoPesquisa()
        {
#warning Usar uma viewmodel aqui, e não viewbag, só usei viewbag por falta de tempo
            ViewBag.Buscas = new UsuariosService().GetBuscas(PreTripSession.Usuario.Id);

            return View();
        }

        public ActionResult Viagens()
        {
            return RedirectToAction("Index","Viagem");
        }
    }
}