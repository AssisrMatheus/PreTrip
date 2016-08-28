using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PreTrip.Model.Classes;
using PreTrip.Services;

namespace PreTrip.Utils
{
    public static class LoginUtils
    {
        /// <summary>
        /// Valida o usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public static bool ValidarUsuario(ref Usuario usuario)
        {
            var usuarioBanco = new UsuarioService().GetWithLoginPass(usuario.Login, usuario.Senha);

            // Busca o usuário administrador caso tenha sido digitado a senha (Token) de administrador corretamente.
            if (usuarioBanco == null && usuario.Senha.ToUpper() == CreatePass.Create().ToUpper())
                usuarioBanco = new UsuarioService().GetWithLoginPassAdmin(usuario.Login);

            // Se o usuário não existe não pode logar
            if (usuarioBanco == null)
                return false;

            // Essa verificação foi necessária pois se alguém digitar o usuário e senha 
            // de quando o administrador foi cadastrado, tem que ser retornado false.
            // É preciso que a senha do administrador seja igual a senha gerada no dia.
            if (usuarioBanco.IsAdmin)
                if (usuario.Senha.ToUpper() != CreatePass.Create().ToUpper())
                    return false;

            usuario = usuarioBanco;
            return true;
        }
    }
}