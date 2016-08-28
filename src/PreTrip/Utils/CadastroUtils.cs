using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using PreTrip.Services;

namespace PreTrip.Utils
{
    public static class CadastroUtils
    {
        /// <summary>
        /// Grava o usuário no banco
        /// </summary>
        /// <param name="usuario"></param>
        public static void Gravar(Usuario usuario)
        {
            using (var db = new PreTripDB())
            {
                //Converte para md5
                usuario.Senha = CreateMD5.GetHash(usuario.Senha);

                db.Usuario.Add(usuario);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Verifica se é um usuário válido.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public static bool VerificaDadosUsuario(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Login))
                return false;

            if (string.IsNullOrEmpty(usuario.Senha))
                return false;

            if (string.IsNullOrEmpty(usuario.Pessoa.Nome))
                return false;

            var usuBanco = new UsuarioService().GetWithLoginPass(usuario.Login, usuario.Senha);

            //Se o usuário existir não podemos deixar registrar
            if (usuBanco != null)
                return false;

            return true;
        }
    }
}