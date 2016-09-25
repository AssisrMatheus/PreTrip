using PreTrip.Model.Classes;
using PreTrip.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreTrip.ViewModel
{
    public class UsuariosViewModel
    {
        public IEnumerable<Pedido> Carrinho
        {
            get
            {
                return PreTripSession.Carrinho;
            }

            set
            {
                PreTripSession.Carrinho = value;
            }

        }

        public double PrecoTotal
        {
            get
            {
                if(this.Carrinho != null)
                    return this.Carrinho.Select(p => p.PrecoFinal * p.Quantidade).Sum();

                return 0;
            }
        }

        public string CupomDesconto;
    }
}