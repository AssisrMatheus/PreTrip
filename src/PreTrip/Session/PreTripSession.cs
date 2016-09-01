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
                return (Usuario)HttpContext.Current.Session["Usuario"];
            }

            set
            {
                HttpContext.Current.Session["Usuario"] = value;
            }
        }
    }
}