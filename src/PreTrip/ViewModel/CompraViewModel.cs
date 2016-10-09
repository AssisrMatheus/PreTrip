using PreTrip.Model.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreTrip.ViewModel
{
    public class CompraViewModel
    {
        public IEnumerable<Pedido> Pedidos { get; set; }

        public double PrecoCompra { get; set; }
    }
}