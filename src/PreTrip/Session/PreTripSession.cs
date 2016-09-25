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
                //Se a sessão ainda não foi iniciada
                if (HttpContext.Current.Session != null)
                {
                    var carrinho = HttpContext.Current.Session["Carrinho"];

                    //Se já existe carrinho na sessão retorna
                    if (carrinho != null)
                        return (IEnumerable<Pedido>)carrinho;
                    else
                    {
                        //Se não existe na sessão, cria um vazio
                        HttpContext.Current.Session["Carrinho"] = new List<Pedido>();
                        //Retorna o recém criado
                        return (IEnumerable<Pedido>)HttpContext.Current.Session["Carrinho"];
                    }
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