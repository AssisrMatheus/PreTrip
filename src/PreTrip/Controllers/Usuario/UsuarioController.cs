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
using PreTrip.ViewModel;
using PreTrip.Attributes;

namespace PreTrip.Controllers
{
    /// <summary>
    /// Controller das views do usuário comum
    /// </summary>
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [UsuarioLogado]
        public ActionResult Index()
        {
            return View();
        }

        [UsuarioLogado]
        public ActionResult AlterarUsuario()
        {
            return View();
        }

        [HttpPost]
        [UsuarioLogado]
        public ActionResult AlterarUsuario(Usuario usuario, string oldSenha)
        {
#warning Falta terminar muita coisa aqui ainda.
            if (string.IsNullOrEmpty(usuario.Senha))
                usuario.Senha = oldSenha;

            var serviceUsuario = new UsuariosService();
            serviceUsuario.Alterar(usuario);

            return RedirectToAction("Index", "Usuario");
        }

        [UsuarioLogado]
        public ActionResult Empresas()
        {

#warning Aqui o usuário está podendo ver TODAS as empresas. Não podemos deixar ele cadastrar empresa sozinha, ele precisa cadastrar uma viagem com empresa, para poder fazer o join de Viagens que ele tem pra poder pegar as empresas e poder listar
            ViewBag.Empresas = new EmpresasService().GetAll(PreTripSession.Usuario.Id);

            return View();
        }

        [UsuarioLogado]
        public ActionResult CadastrarEmpresa()
        {         
            return View();
        }

        [HttpPost]
        [UsuarioLogado]
        public ActionResult CadastrarEmpresa(Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                var empresaService = new EmpresasService();

                empresa.UsuarioId = PreTripSession.Usuario.Id;
                empresa.Usuario = PreTripSession.Usuario;

                empresaService.Inserir(empresa);
                ModelState.Clear();
            }

            //Sem o redirect os dados do formulário nao estavam limpando.
            return RedirectToAction("Empresas");
        }


        [UsuarioLogado]
        public ActionResult Viagens()
        {
            var viewModel = new UsuariosViewModel()
            {
                Viagens = new ViagensService().GetAllUser(PreTripSession.Usuario.PessoaId)
            };

            return View(viewModel);
        }

        [UsuarioLogado]
        public ActionResult Pedidos()
        {
            var viewModel = new UsuariosViewModel()
            {
                Pedidos = PreTripSession.Usuario.Pessoa.Pedidos
            };

            return View(viewModel);
        }

        [UsuarioLogado]
        public ActionResult Interesses()
        {
            var listaInteresses = new CidadesService().GetAllDistinctCity();

            return View(listaInteresses);
        }

        [HttpPost]
        [UsuarioLogado]
        public ActionResult Interesses(List<string> cidadesEscolhidas)
        {
#warning utilizar validação via modelstate
            if (cidadesEscolhidas == null || !cidadesEscolhidas.Any())
                return RedirectToAction("Index", "Usuario");

            var listaInteresses = cidadesEscolhidas
                .Select(c => new Interesse()
                {
                    PessoaId = PreTripSession.Usuario.Pessoa.Id,
                    Pessoa = PreTripSession.Usuario.Pessoa,
                    Cidade = c
                }).ToList();            

            new InteresseService().InsertOrUpdate(listaInteresses);

            return RedirectToAction("Index", "Usuario");
        }

        [UsuarioLogado]
        public ActionResult HistoricoPesquisa()
        {
#warning Usar uma viewmodel aqui, e não viewbag, só usei viewbag por falta de tempo
            ViewBag.Buscas = new UsuariosService().GetBuscas(PreTripSession.Usuario.Id);

            return View();
        }

        [UsuarioLogado]
        public ActionResult Logout()
        {
            PreTripSession.Usuario = null;
            return RedirectToAction("Index", "Home");
        }

        [UsuarioLogado]
        public ActionResult CadastrarEndereco()
        {
            return View();
        }

        [HttpPost]
        [UsuarioLogado]
        public ActionResult CadastrarEndereco(Endereco endereco)
        {
            if (ModelState.IsValid) 
            {
                var enderecoService = new EnderecosService();
                endereco.UsuarioId = PreTripSession.Usuario.Id;
                enderecoService.Gravar(endereco);
                return RedirectToAction("Index", "Usuario");
            }

            return View(endereco);
        }

        public ActionResult AddViagemCarrinho(Viagem viagem)
        {
            //Se existe uma viagem igual
            if (PreTripSession.Carrinho != null && PreTripSession.Carrinho.Pedidos.Where(p => p.Viagem.Id == viagem.Id).Any())
            {
                PreTripSession.Carrinho.Pedidos //Para todos os pedidos
                    .Where(p => p.Viagem.Id == viagem.Id) //Onde a viagem do pedido é a mesma da viagem sendo adicionada
                    .FirstOrDefault() //Pego o primeiro pois sei que só veio 1(pelo id)
                    .Quantidade += 1; //Adiciono 1 à quantidade
            }
            else
            {
                var pedidos = PreTripSession.Carrinho.Pedidos.ToList();
                pedidos.Add(new Pedido(viagem));

                PreTripSession.Carrinho.Pedidos = pedidos;
            }

            return RedirectToAction("MeuCarrinho", "Usuario");
        }
        
        public ActionResult MeuCarrinho()
        {
            var viewModel = new UsuariosViewModel()
            {
                Carrinho = PreTripSession.Carrinho
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddCupom(UsuariosViewModel viewModel)
        {
            if (viewModel.Carrinho.CupomDesconto == "TESTE")
            {
                PreTripSession.Carrinho.PrecoDesconto += 30.50;
            }

            return RedirectToAction("MeuCarrinho");
        }

        [UsuarioLogado]
        public ActionResult ConfirmarCompra()
        {
            if (PreTripSession.Carrinho == null || PreTripSession.Usuario.Pessoa.ContaBancaria.Saldo < PreTripSession.Carrinho.PrecoFinal)
                return RedirectToAction("MeuCarrinho");

            var carrinho = PreTripSession.Carrinho;
            var usuario = PreTripSession.Usuario;

            //Adiciono os pedidos do carrinho aos pedidos já feito pela pessoa
            var pedidos = usuario.Pessoa.Pedidos.ToList();
            pedidos.AddRange(carrinho.Pedidos);
            usuario.Pessoa.Pedidos = pedidos;

            usuario.Pessoa.ContaBancaria.Saldo -= carrinho.PrecoFinal;

            //Salvo as alterações
            new UsuariosService().SalvarModificacoes(usuario);

            PreTripSession.Carrinho = null;
            PreTripSession.Usuario = usuario;
            return View();
        }
    }
}