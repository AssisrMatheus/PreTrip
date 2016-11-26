using PreTrip.Model.Classes;
using PreTrip.ViewModel;
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
                if(value == null)
                {
                    HttpContext.Current.Session.Clear();
                }
                else
                {
                    HttpContext.Current.Session["Usuario"] = value;
                }
            }
        }

        public static CarrinhoViewModel Carrinho
        {
            get
            {
                //Se a sessão ainda não foi iniciada
                if (HttpContext.Current.Session != null)
                {
                    var carrinho = HttpContext.Current.Session["Carrinho"];

                    //Se já existe carrinho na sessão retorna
                    if (carrinho != null)
                        return (CarrinhoViewModel)carrinho;
                }

                //Se não existe na sessão, cria um vazio
                HttpContext.Current.Session["Carrinho"] = new CarrinhoViewModel();
                //Retorna o recém criado
                return (CarrinhoViewModel)HttpContext.Current.Session["Carrinho"];
            }

            set
            {
                HttpContext.Current.Session["Carrinho"] = value;
            }
        }

        public static CompraViewModel UltimaCompra
        {
            get
            {
                //Se a sessão ainda não foi iniciada
                if (HttpContext.Current.Session != null)
                {
                    var ultimaCompra = HttpContext.Current.Session["UltimaCompra"];

                    //Se já existe carrinho na sessão retorna
                    if (ultimaCompra != null)
                        return (CompraViewModel)ultimaCompra;
                }

                //Se não existe na sessão, cria um vazio
                HttpContext.Current.Session["UltimaCompra"] = new CompraViewModel();
                //Retorna o recém criado
                return (CompraViewModel)HttpContext.Current.Session["UltimaCompra"];
            }

            set
            {
                HttpContext.Current.Session["UltimaCompra"] = value;
            }
        }
    }
}