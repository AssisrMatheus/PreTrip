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
        public CarrinhoViewModel Carrinho { get; set; }
        
        public IEnumerable<Viagem> Viagens { get; set; }

        public IEnumerable<Pedido> Pedidos { get; set; }

        public float NovoSaldo { get; set; }
    }
}