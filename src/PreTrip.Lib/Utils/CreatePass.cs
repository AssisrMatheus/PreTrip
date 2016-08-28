using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreTrip.Lib.Utils
{
    public class CreatePass
    {
        private CreatePass()
        {
        }

        public static string Create()
        {
            var nomeDia = DateTime.Now.DayOfWeek.ToString().Substring(0, 3);
            var numMes = DateTime.Now.Month.ToString().Count() < 2 ? ("0"+DateTime.Now.Month.ToString()) : DateTime.Now.Month.ToString();
            var soma = (DateTime.Now.Day + DateTime.Now.Hour);
            var diaHora = soma.ToString().Count() < 2 ? ("0" + soma.ToString()) : soma.ToString();

            return string.Format("{0}{1}{2}", nomeDia, numMes, diaHora);
        }
    }
}