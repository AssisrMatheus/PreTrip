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
using PreTrip.Services.Interesse;
using PreTrip.Services.Viagens;
using PreTrip.ViewModel;
using PreTrip.Attributes;
using PreTrip.Services.ControleFinanceiro;

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
#warning usar viewmodel e não viewbag
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
                Viagens = new ViagensService().GetAllFromPessoa(PreTripSession.Usuario.Pessoa.Id)
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
            var listaInteresses = new InteresseService().GetAllCities();

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
                endereco.Usuario = PreTripSession.Usuario;

                enderecoService.Gravar(endereco);
                return RedirectToAction("Index", "Usuario");
            }

            return View(endereco);
        }

        public ActionResult AddViagemCarrinho(int id)
        {
            var viagemPessoa = new ViagensService().GetViagem(id);
            //Se existe uma viagem igual
            if (PreTripSession.Carrinho != null && PreTripSession.Carrinho.Pedidos.Where(p => p.Viagem.Id == viagemPessoa.Id).Any())
            {
                PreTripSession.Carrinho.Pedidos //Para todos os pedidos
                    .Where(p => p.Viagem.Id == viagemPessoa.Id) //Onde a viagem do pedido é a mesma da viagem sendo adicionada
                    .FirstOrDefault() //Pego o primeiro pois sei que só veio 1(pelo id)
                    .Quantidade += 1; //Adiciono 1 à quantidade
            }
            else
            {
                var pedidos = PreTripSession.Carrinho.Pedidos.ToList();
                pedidos.Add(new Pedido(viagemPessoa));

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

        public ActionResult AdicionarSaldo()
        {
            return View();
        }

        public ActionResult ControleFinanceiro()
        {
            // Lista de vendas
            var vendas = new ControleFinanceiroService().GetAllVendas(PreTripSession.Usuario.Pessoa.Id);

            // Lista de compras
            var compras = new ControleFinanceiroService().GetAllCompras(PreTripSession.Usuario.Pessoa.Id);

            var listaRelatorioFinanceiro = new List<RelatorioFinanceiro>();

            if (vendas != null && vendas.Count > 0)
                foreach (var venda in vendas)
                    this.AddVendasRelatorioFinanceiro(listaRelatorioFinanceiro, venda);
                
            if (compras != null && compras.Count > 0)
                foreach (var compra in compras)
                    this.AddComprasRelatorioFinanceiro(listaRelatorioFinanceiro, compra);

            return View(listaRelatorioFinanceiro);
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

            var pedidos = new List<Pedido>();

            //Adiciono os pedidos do carrinho aos pedidos já feito pela pessoa
            if (usuario.Pessoa.Pedidos != null && usuario.Pessoa.Pedidos.Any())
                pedidos = usuario.Pessoa.Pedidos.ToList();

            pedidos.AddRange(carrinho.Pedidos);
            usuario.Pessoa.Pedidos = pedidos;

            usuario.Pessoa.ContaBancaria.Saldo -= carrinho.PrecoFinal;

            this.AtualizarSaldoCriadorViagem(pedidos);

            //Salvo as alterações
            new UsuariosService().Gravar(usuario);

            PreTripSession.Carrinho = null;
            PreTripSession.Usuario = usuario;

            return View(new CompraViewModel() { Pedidos = carrinho.Pedidos, PrecoCompra = carrinho.PrecoFinal });
        }

        private void AtualizarSaldoCriadorViagem(IEnumerable<Pedido> pedidos)            
        {
            //Junto todos os pedidos por organizador e somo o lucro para diminuir o foreach e os acessos ao banco
            var pedidosTotal = pedidos
                .GroupBy(x => x.Viagem.Pessoa.Id)
                .Select(p => new
                {
                    IdOrganizador = p.Key,
                    Lucro = p.Sum(x => x.PrecoFinal)
                });

            var service = new UsuariosService();

            foreach (var pedido in pedidosTotal)
            {
                //Busco o organizador
                var pessoaCriadoraViagem = new UsuariosService().GetUsuarioById(pedido.IdOrganizador);
                
                pessoaCriadoraViagem.Pessoa.ContaBancaria.Saldo += pedido.Lucro;

                //Salva no banco
                service.Gravar(pessoaCriadoraViagem);
            }
        }

        /// <summary>
        /// Adiciona as vendas na lista do relatório financeiro.
        /// </summary>
        /// <param name="listaRelatorioFinanceiro"></param>
        /// <param name="venda"></param>
        private void AddVendasRelatorioFinanceiro(List<RelatorioFinanceiro> listaRelatorioFinanceiro, Viagem venda)
        {
            var relatorioVenda = new RelatorioFinanceiro()
            {
                Descricao = venda.Descricao,
                PrecoViagem = venda.PrecoPassagem,
                Quantidade = 1,
                Venda = true
            };

            // Verifica se na lista já existe alguem com a mesma descrição
            if (listaRelatorioFinanceiro.Exists(x => x.Descricao == venda.Descricao))
            {
                // Se já existir, soma +1 na quantidade.
                listaRelatorioFinanceiro.ForEach(x =>
                {
                    if (x.Descricao == venda.Descricao)
                        x.Quantidade++;
                });
            }
            else
            {
                // Se não existir, adiciona a nova venda.
                listaRelatorioFinanceiro.Add(relatorioVenda);
            }
        }

        /// <summary>
        /// Adiciona as compras na lista do relatório financeiro.
        /// </summary>
        /// <param name="listaRelatorioFinanceiro"></param>
        /// <param name="compra"></param>
        private void AddComprasRelatorioFinanceiro(List<RelatorioFinanceiro> listaRelatorioFinanceiro, Pedido compra)
        {
            var relatorioCompra = new RelatorioFinanceiro()
            {
                Descricao = compra.Viagem.Descricao,
                PrecoViagem = compra.PrecoFinal,
                Quantidade = 1,
                Venda = false
            };

            // Verifica se na lista já existe alguem com a mesma descrição
            if (listaRelatorioFinanceiro.Exists(x => x.Descricao == compra.Viagem.Descricao))
            {
                // Se já existir, soma +1 na quantidade.
                listaRelatorioFinanceiro.ForEach(x =>
                {
                    if (x.Descricao == compra.Viagem.Descricao)
                        x.Quantidade++;
                });
            }
            else
            {
                // Se não existir, adiciona a nova compra.
                listaRelatorioFinanceiro.Add(relatorioCompra);
            }
        }
    }
}