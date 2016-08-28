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
        public static bool TodosCamposValidos(Object objeto)
        {           
            PropertyInfo[] atributos = objeto.GetType().GetProperties();

            foreach (PropertyInfo atributo in atributos)
            {                
                Object atributoObjeto = atributo.GetValue(objeto);

                if (!(isAtributoEndereco(atributo))) //adicionado apenas para nao validar o campo endereco no caso de empresa
                {
                    if (atributoObjeto == null || string.IsNullOrWhiteSpace(atributoObjeto.ToString()))
                    {
                        return false;
                    }
                }                
            }

            return true;
        }

        private static bool isAtributoEndereco(PropertyInfo atributo)
        {
            //adicionado apenas para nao validar o campo endereco no caso de empresa
              if ((atributo.PropertyType.ToString().Equals("PreTrip.Model.Classes.Endereco"))) 
              {
                  return true;
              }

              return false;
        }
    }
}
