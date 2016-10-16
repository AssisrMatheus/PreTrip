using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PreTrip.Model.Classes;
using PreTrip.Model.Context;

namespace PreTrip.Services.ControleFinanceiro
{
    public class ControleFinanceiroService
    {
        private PreTripDB db { get; set; }

        public ControleFinanceiroService()
        {
            this.db = new PreTripDB();
        }

        public List<Viagem> GetAllVendas(int idPessoa)
        {
            var viagens = db.Viagens;

            return viagens.Where(x => x.Pessoa.Id == idPessoa).ToList();
        }

        public List<Pedido> GetAllCompras(int idPessoa)
        {
            var pedidos = db.Pedidos;

            return pedidos.Where(x => x.Pessoa.Id == idPessoa).ToList();
        }
    }

#warning coloquei essa classe aqui pq fiquei perdido em onde colocar ela kkk
    public class RelatorioFinanceiro
    {
        public string Descricao { get; set; }

        public double PrecoViagem { get; set; }

        public int Quantidade { get; set; }

        public bool Venda { get; set; }

        public double GetValorTotalVendido()
        {
            return this.Quantidade * this.PrecoViagem;
        }
    }
}