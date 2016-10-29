using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

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
    }
}