using PreTrip.Lib.Utils;
using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
                var usuarios = db.Usuarios.ToList();

                if (filtro != null && usuarios.Any())
                    return usuarios.Where(filtro);
                else
                    return usuarios;
            }
        }

        public IEnumerable<Interesse> GetUsuarioInteresses(Func<Usuario, bool> filtro = null)
        {
#warning Falta terminar. Verificar no banco pq não está trazendo o id do usuario.
            using (var db = new PreTripDB())
            {
                var listaInteresses = db.UsuarioInteresses.ToList();

                return listaInteresses;
            }
        }

        /// <summary>
        /// Grava o usuário no banco
        /// </summary>
        /// <param name="usuario"></param>
        public void Inserir(Usuario usuario)
        {
            //Se é um usuário válido, cadastra.
            if (this.ValidaNovoUsuario(usuario))
                using (var db = new PreTripDB())
                {
                    //Converte para md5
                    usuario.Senha = CreateMD5.GetHash(usuario.Senha);

                    db.Usuarios.Add(usuario);
                    db.SaveChanges();
                }
        }

        /// <summary>
        /// Altera um usuário no banco
        /// </summary>
        /// <param name="usuario"></param>
        public void Alterar(Usuario usuario)
        {
            if (!this.ValidaNovoUsuario(usuario))
                using (var db = new PreTripDB())
                {
                    // Converte para md5
                    usuario.Senha = CreateMD5.GetHash(usuario.Senha);

                    db.Usuarios.AddOrUpdate(usuario);
                    db.SaveChanges();
                }
        }

        /// <summary>
        /// Verifica se é um usuário válido.
        /// Retorna True caso não exista o usuário no banco
        /// retorna False caso exista.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool ValidaNovoUsuario(Usuario usuario)
        {
            return !this.GetUsers(u => u.Login == usuario.Login).Any();
        }

        public IEnumerable<Busca> GetBuscas(int id)
        {
            using (var db = new PreTripDB())
            {
                return db.Buscas.Where(b => b.UsuarioId == id).ToList();
            }
        }
    }
}
