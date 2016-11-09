using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PreTrip.ViewModel;
using PreTrip.Session;
using PreTrip.Services.Usuarios;

namespace PreTrip.Services.Pedidos
{
    public class PedidosService
    {
        private PreTripDB db { get; set; }

        public PedidosService()
        {
            this.db = new PreTripDB();
        }

        private IQueryable<Pedido> pedidos
        {
            get
            {
                return this.db.Pedidos
                    .Include(x => x.Viagem)
                    .Include(x => x.Viagem.Origem)
                    .Include(x => x.Viagem.Destino)
                    .Include(x => x.Viagem.Empresa)
                    .Include(x => x.Viagem.Veiculo)
                    .Include(x => x.Viagem.Pessoa)
                    .Include(x => x.Viagem.Pessoa.ContaBancaria)
                    .Include(x => x.Pessoa)
                    .Include(x => x.Pessoa.ContaBancaria);
            }
        }

        public IEnumerable<Pedido> GetPedidos(Func<Pedido, bool> filtro = null)
        {
            if (filtro != null)
                return this.pedidos.Where(filtro).ToList();
            else
                return this.pedidos.ToList();
        }

        public CompraViewModel RegistrarCompra(CompraViewModel viewModel)
        {
            var carrinho = PreTripSession.Carrinho;
            var usuario = PreTripSession.Usuario;
            var pedidos = new List<Pedido>();

            //Se o usuário tem pedidos, adiciono à lista de pedidos
            if (usuario.Pessoa.Pedidos != null && usuario.Pessoa.Pedidos.Any())
                pedidos = usuario.Pessoa.Pedidos.ToList();

            //Adiciono os pedidos dos carrinho à lista de pedidos
            pedidos.AddRange(carrinho.Pedidos);

            //Substituo todos os pedidos de pessoa
            usuario.Pessoa.Pedidos = pedidos;

            //Diminuo o saldo da pessoa pelo preço total da compra
            usuario.Pessoa.ContaBancaria.Saldo -= carrinho.PrecoFinal;

            viewModel.Pedidos = carrinho.Pedidos;
            viewModel.PrecoCompra = carrinho.PrecoFinal;

            //Registra aos donos das viagens as suas vendas;
            this.RegistrarVenda(pedidos);

            //Salvo as alterações do usuário no banco
            var service = new UsuariosService();
            service.Gravar(usuario);

            //Busco os pedidos para mostrar na tela
            carrinho.Pedidos = this.GetPedidos(x => x.PessoaId == usuario.Pessoa.Id);

            //Limpo o carrinho
            PreTripSession.Carrinho = null;

            //Guardo o novo usuário com os novos pedidos e o novo saldo na sessão novamente
            PreTripSession.Usuario = service.GetUsuarioById(usuario.Id);

            viewModel.Sucesso = true;

            return viewModel;
        }

        private void RegistrarVenda(IEnumerable<Pedido> pedidos)
        {
            //Junto todos os pedidos por organizador(vendedor) e somo o lucro de uma vez
            //para diminuir o foreach e os acessos ao banco
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
                var pessoaCriadoraViagem = service.GetUsuarioById(pedido.IdOrganizador);

                pessoaCriadoraViagem.Pessoa.ContaBancaria.Saldo += pedido.Lucro;

                //Salva no banco
                service.Gravar(pessoaCriadoraViagem);
            }
        }

        private CompraViewModel ValidarCartao(CompraViewModel viewModel)
        {


            return viewModel;
        }
    }
}