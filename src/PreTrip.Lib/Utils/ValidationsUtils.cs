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
        public static bool AlgumCampoPreenchido(object objeto)
        {
            PropertyInfo[] atributos = objeto.GetType().GetProperties();

            foreach (PropertyInfo atributo in atributos)
            {
                Object atributoObjeto = atributo.GetValue(objeto);
                
                if (!atributo.ToString().Contains("System.String"))
                {
                    if (!atributo.ToString().Contains("DateTime") && AlgumCampoNumericoPreenchido(atributoObjeto))
                    {
                        return true;
                    }
                }
                else
                {
                    if (!atributo.ToString().Contains("DateTime") && AlgumCampoStringPreenchido(atributoObjeto))
                    {
                        return true;
                    }
                }

            }

            return false;
        }

        private static bool AlgumCampoStringPreenchido(object atributoObjeto)
        {
            if (atributoObjeto != null && !string.IsNullOrWhiteSpace(atributoObjeto.ToString()))
            {
                return true;
            }
            return false;
        }

        private static bool AlgumCampoNumericoPreenchido(object atributoObjeto)
        {
            if (atributoObjeto != null && Convert.ToDouble(atributoObjeto) != 0)
            {
                return true;
            }
            return false;
        }
    }
}
