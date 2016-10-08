using PreTrip.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PreTrip.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class UsuarioLogadoAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //Isso é chamado para testar se está autorizado ou não.
            //Um método é não autorizado quando o usuário não está logado
            return (PreTripSession.Usuario != null);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //Isso é chamado quando a função acima retorna false
            throw new Exception("Usuario precisa estar logado");
        }
    }
}