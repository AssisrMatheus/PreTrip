using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PreTrip.Model.Classes;
using PreTrip.Services.Cidades;
using PreTrip.Services.Empresas;
using PreTrip.Services.Usuarios;
using PreTrip.Session;
using PreTrip.Services.Enderecos;
using PreTrip.Services.Interesse;
using PreTrip.Services.Viagens;

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
            var listaInteresses = new CidadesService().GetAllDistinctCity();

            return View(listaInteresses);
        }

        [HttpPost]
        public ActionResult Interesses(List<string> cidadesEscolhidas)
        {
            var listaInteresses = new List<Interesse>();

            var idUser = PreTripSession.Usuario.Id;

            foreach (var cidade in cidadesEscolhidas)
            {
                var interesse = new Interesse()
                {
                    UsuarioId = idUser,
                    Cidade = cidade,
                    Usuario = PreTripSession.Usuario
                };

                listaInteresses.Add(interesse);
            }

            new InteresseService().InsertOrUpdate(listaInteresses);

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
            if (CarrinhoContem(viagem))
            {
                pedidoExistente.Quantidade += 1;
                var pedidos = PreTripSession.Carrinho.ToList();
                pedidos.Add(pedidoExistente);
                PreTripSession.Carrinho = pedidos;
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
       
        public bool CarrinhoContem(Viagem viagem)
        {
            var pedidos = PreTripSession.Carrinho;

            if (pedidos != null)
            {
                foreach (var pedido in pedidos)
                {
                    if (pedido.ViagemId == viagem.Id)
                    {
                        pedidos.ToList().Remove(pedido);
                        pedidoExistente = pedido;
                        return true;
                    }
                }
            }
            else
            {
                pedidos = new List<Pedido>();
                PreTripSession.Carrinho = pedidos;
            }

            return false;
        }

        public Pedido CriarNovoPedido(Viagem viagem)
        {
            Pedido pedido = new Pedido();
            pedido.DtHrRealizacao = new DateTime();
            pedido.ViagemId = viagem.Id;
            pedido.Quantidade = 1;
            pedido.Viagem = viagem;

            return pedido;
        }
        
        public ActionResult MeuCarrinho()
        {
            if (PreTripSession.Carrinho == null)
            {
                pedidos = new List<Pedido>();
                PreTripSession.Carrinho = pedidos;
            }           
            return View();
        }

        public ActionResult CadastroInteresses()
        {
            return View();
        }
    }
}