using PreTrip.Lib.Utils;
using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PreTrip.Services.Usuarios
{
    public class UsuariosService
    {
        public IEnumerable<Usuario> GetUsers(Func<Usuario, bool> filtro = null)
        {
            using (var db = new PreTripDB())
            {
                //Pega um usuário com seu objeto pessoa preenchido
                var usuarios = from usu in db.Usuario.ToList()
                            join pes in db.Pessoa.ToList()
                            on usu.Pessoa equals pes
                            select new Usuario()//Aqui seto os parâmetros que virão no select(quais colunas)
                            {
                                IsAdmin = usu.IsAdmin,
                                Pessoa = pes,
                                Email = usu.Email,
                                Login = usu.Login,
                                Id = usu.Id,
                                Pedidos = usu.Pedidos,
                                Senha = usu.Senha
                            };

                //Como castei na query acima, posso fazer o filtro usando o model Usuario
                if (filtro != null && usuarios.Any())
                    return usuarios.Where(filtro);
                else
                    return usuarios;
            }
        }
        
        /// <summary>
        /// Grava o usuário no banco
        /// </summary>
        /// <param name="usuario"></param>
        public void Inserir(Usuario usuario)
        {
            //Se o usuário não é valido, cancela.
            if (!this.ValidaNovoUsuario(usuario))
                return;

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
        public bool ValidaNovoUsuario(Usuario usuario)
        {
            var usuBanco = this.GetUsers(u => u.Login == usuario.Login);

            //Se o usuário existir não podemos deixar registrar
            if (usuBanco.Any())
            {
                return false;
            }   

            return true;
        }
    }
}
