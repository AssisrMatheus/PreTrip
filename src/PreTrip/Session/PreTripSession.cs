using PreTrip.Model.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreTrip.Session
{
    public class PreTripSession
    {
        private PreTripSession()
        {

        }

        public static Usuario Usuario
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    var usu = HttpContext.Current.Session["Usuario"];
                    if (usu != null)                        
                        return (Usuario)usu;
                }
                return null;
            }

            set
            {
                HttpContext.Current.Session["Usuario"] = value;
            }
        }

        public static IEnumerable<Pedido> Carrinho
        {
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    var carrinho = HttpContext.Current.Session["Carrinho"];
                    if (carrinho != null)
                        return (IEnumerable<Pedido>)carrinho;
                }
                return null;
            }

            set
            {
                HttpContext.Current.Session["Carrinho"] = value;
            }
        }
    }
}