using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PreTrip.Model.Classes;
using PreTrip.Services.Empresas;
using PreTrip.Services.Usuarios;
using PreTrip.Session;
using PreTrip.Services.Enderecos;
using PreTrip.Services.Viagens;
using PreTrip.ViewModel;

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

        public ActionResult Logout()
        {
            PreTripSession.Usuario = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CadastrarEndereco()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GravarEndereco(Endereco endereco)
        {
            if (ModelState.IsValid) 
            {
                var enderecoService = new EnderecosService();
                endereco.UsuarioId = PreTripSession.Usuario.Id;
                enderecoService.Gravar(endereco);
            }

            return RedirectToAction("Index", "Usuario");
        }

        private Pedido pedidoExistente { get; set; }
        private List<Pedido> pedidos { get; set; }
        public ActionResult AddViagemCarrinho(Viagem viagem)
        {
            //Se existe uma viagem igual
            if (PreTripSession.Carrinho != null && PreTripSession.Carrinho.Where(p => p.Viagem.Id == viagem.Id).Any())
            {
                PreTripSession.Carrinho //Para todos os pedidos
                    .Where(p => p.Viagem.Id == viagem.Id) //Onde a viagem do pedido é a mesma da viagem sendo adicionada
                    .FirstOrDefault() //Pego o primeiro pois sei que só veio 1(pelo id)
                    .Quantidade += 1; //Adiciono 1 à quantidade
            }
            else
            {
                Pedido novoPedido = CriarNovoPedido(viagem);

                var pedidos = PreTripSession.Carrinho.ToList();
                pedidos.Add(novoPedido);
                PreTripSession.Carrinho = pedidos;
            }

            return RedirectToAction("MeuCarrinho", "Usuario");
        }

        public Pedido CriarNovoPedido(Viagem viagem)
        {
            return new Pedido()
            {
                DtHrRealizacao = DateTime.Now,
                Viagem = viagem,
                ViagemId = viagem.Id,
                Quantidade = 1,
                PrecoFinal = viagem.PrecoPassagem
            };
        }
        
        public ActionResult MeuCarrinho()
        {
            if (PreTripSession.Carrinho == null)
            {
                pedidos = new List<Pedido>();
                PreTripSession.Carrinho = pedidos;
            }           
            return View(new UsuariosViewModel());
        }

        [HttpPost]
        public ActionResult MeuCarrinho(UsuariosViewModel viewModel)
        {
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddCupom(UsuariosViewModel viewModel)
        {
            if (viewModel.CupomDesconto == "TESTE")
            {
                viewModel.PrecoDesconto += 30.50;
            }

            return RedirectToAction("MeuCarrinho", "Usuario", viewModel);
        }

        public ActionResult CadastroInteresses()
        {
            return View();
        }
    }
}