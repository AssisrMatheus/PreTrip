using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PreTrip.Lib.Utils
{
    public class ValidationsUtils
    {
        public static bool AlgumCampoPreenchido(Object objeto)
        {           
            PropertyInfo[] atributos = objeto.GetType().GetProperties();

            foreach (PropertyInfo atributo in atributos)
            {                
                Object atributoObjeto = atributo.GetValue(objeto);
                atributo.GetType().ToString();
                if (atributo.GetType().ToString() != "string")
                {
                    return AlgumCampoNumericoPreenchido(atributoObjeto);
                }
                else
                {
                    return AlgumCampoStringPreenchido(atributoObjeto);
                }     
                                             
            }

            return false;
        }

        private static bool AlgumCampoStringPreenchido(Object atributoObjeto)
        {
            if (atributoObjeto != null || !string.IsNullOrWhiteSpace(atributoObjeto.ToString()) || !atributoObjeto.ToString().Equals("0"))
            {
                return true;
            }
            return false;
        }

        private static bool AlgumCampoNumericoPreenchido(Object atributoObjeto)
        {
            if (atributoObjeto != null || Convert.ToDouble(atributoObjeto) != 0)
            {
                return true;
            }
            return false;
        }     
    }
}
