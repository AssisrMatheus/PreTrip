using PreTrip.Model.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreTrip.ViewModel
{
    public class CarrinhoViewModel
    {
        public IEnumerable<Pedido> Pedidos { get; set; }

        public double PrecoTotal
        {
            get
            {
                if (this.Pedidos != null)
                    return this.Pedidos.Select(p => p.PrecoFinal * p.Quantidade).Sum();

                return 0;
            }
        }

        public double PrecoDesconto { get; set; }

        public double PrecoFinal
        {
            get
            {
                return PrecoTotal - PrecoDesconto;
            }
        }

        public string CupomDesconto { get; set; }

        public CarrinhoViewModel()
        {
            this.Pedidos = new List<Pedido>();
        }
    }
}