using PreTrip.Model.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreTrip.ViewModel
{
    public class CompraViewModel
    {
        public bool Sucesso { get; set; }

        public string Informacao { get; set; }

        public IEnumerable<Pedido> Pedidos { get; set; }

        public double PrecoCompra { get; set; }

        public int IdCartao { get; set; }

        public Cartao Cartao { get; set; }

        public int NumParcelas { get; set; }
    }
}