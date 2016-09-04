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
    }
}