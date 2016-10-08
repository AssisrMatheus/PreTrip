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
        public CarrinhoViewModel Carrinho
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
    }
}