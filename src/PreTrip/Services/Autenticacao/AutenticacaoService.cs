using PreTrip.Lib.Utils;
using PreTrip.Services.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreTrip.Services.Autenticacao
{
    public class AutenticacaoService
    {
        public bool Autenticar(string login, string senha)
        {
            var service = new UsuariosService();

            //Pegamos somente por login e somente se for admin.
            var usuAdmin = service.GetUsers(u => u.Login == login && u.IsAdmin).FirstOrDefault();

            //Se existe um usuario admin com o login
            if (usuAdmin != null)
            {
                //A senha do admin é a senha gerada, se for diferente da digitada
                //ele não é um usuario válido, retorne nulo.
                if (usuAdmin.Senha != senha)
                    return false;
                else
                {
                    //Se a senha digitada foi correta com a gerada é um login de admin válido
                    HttpContext.Current.Session["Usuario"] = usuAdmin;
                    return true;
                }   
            }
            else
            {
                //Se não encontrou nenhum usuario admin com aquele login, é um usuário normal
                //então precisa ter digitado a senha correta também, e retornará nulo se inválido(padrão do first ou default),
                var senhaHash = CreateMD5.GetHash(senha);
                var usuario = service.GetUsers(u => u.Login == login && u.Senha == senhaHash).FirstOrDefault();

                if(usuario != null)
                    HttpContext.Current.Session["Usuario"] = usuario;

                return usuario != null;
            }
        }
    }
}