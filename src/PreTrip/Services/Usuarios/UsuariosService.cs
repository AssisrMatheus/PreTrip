using PreTrip.Lib.Utils;
using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PreTrip.Services.Usuarios
{
    public class UsuariosService
    {
        private PreTripDB db { get; set; }

        public UsuariosService()
        {
            this.db = new PreTripDB();
        }

        public Usuario GetUsuarioById(int pessoaId)
        {
            return db.Usuarios
                .Include(x => x.Pessoa)
                .Include(x => x.Pessoa.ContaBancaria)
                .FirstOrDefault(x => x.Id == pessoaId);
        }

        public Usuario GetUsuarioLoginSenha(string login, string senha)
        {
            return db.Usuarios
                .Include(x => x.Pessoa)
                .Include(x => x.Pessoa.ContaBancaria)
                .FirstOrDefault(x => x.Login == login && x.Senha == senha);
        }

        public IEnumerable<Usuario> GetUsers(Func<Usuario, bool> filtro = null)
        {
            var usuarios = db.Usuarios
                .Include(x => x.Pessoa)
                .Include(x => x.Pessoa.ContaBancaria);

            if (filtro != null)
                return usuarios.Where(filtro).ToList();
            else
                return usuarios.ToList();
        }

        /// <summary>
        /// Grava o usuário no banco
        /// </summary>
        /// <param name="usuario"></param>
        public void Inserir(Usuario usuario)
        {
            //Se é um usuário válido, cadastra.
            if (!this.UsuarioExiste(usuario))
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
            if (this.UsuarioExiste(usuario))
            {
                // Converte para md5
                usuario.Senha = CreateMD5.GetHash(usuario.Senha);

                db.Usuarios.AddOrUpdate(usuario);
                db.SaveChanges();
            }
        }

#warning verificar a necessidade desse método
        public void Gravar(Usuario usuario)
        {
            var usuExistente = db.Usuarios
                .Include(x => x.Pessoa)
                .Include(x => x.Pessoa.ContaBancaria)
                .Where(x => x.Id == usuario.Id).FirstOrDefault();

            //Se existe
            if (usuExistente != null)
            {
                db.Entry(usuExistente).CurrentValues.SetValues(usuario);

                var pessoa = db.Pessoas.Where(x => x.Id == usuario.Pessoa.Id).FirstOrDefault();
                db.Entry(pessoa).CurrentValues.SetValues(usuario.Pessoa);

                var contaBanc = db.ContasBancarias.Where(x => x.Id == usuario.Pessoa.ContaBancaria.Id).FirstOrDefault();
                db.Entry(contaBanc).CurrentValues.SetValues(usuario.Pessoa.ContaBancaria);

                usuExistente.Pessoa = pessoa;
                usuExistente.Pessoa.ContaBancaria = contaBanc;

                usuExistente.Pessoa.Pedidos = usuario.Pessoa.Pedidos;
                usuExistente.Pessoa.Interesses = usuario.Pessoa.Interesses;
                usuExistente.Pessoa.Viagens = usuario.Pessoa.Viagens;

                usuExistente.Pessoa.Viagens.ToList()
                    .ForEach(x => 
                    {
                        db.Viagens.AddOrUpdate(x);
                    });

                usuExistente.Pessoa.Pedidos.ToList()
                    .ForEach(x =>
                    {
                        x.Viagem = null;
                        x.Pessoa = null;
                        db.Pedidos.AddOrUpdate(x);
                    });

                
                usuExistente.Pessoa.Interesses.ToList().ForEach(x => db.Interesses.AddOrUpdate(x));
            }
            else
            {
                db.Usuarios.Add(usuario);
            }

            db.SaveChanges();
        }

        public void GravarSaldo(Usuario usuario)
        {
            var contaExistente = db.ContasBancarias.Where(x => x.Id == usuario.Pessoa.ContaBancaria.Id).FirstOrDefault();

            //Se existe
            if (contaExistente != null)
                contaExistente.Saldo = usuario.Pessoa.ContaBancaria.Saldo;

            db.SaveChanges();
        }

        /// <summary>
        /// Verifica se é um usuário válido.
        /// Retorna True caso não exista o usuário no banco
        /// retorna False caso exista.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool UsuarioExiste(Usuario usuario)
        {
            return this.GetUsers(u => u.Login == usuario.Login).Any();
        }

        public IEnumerable<Busca> GetBuscas(int userId)
        {
            return db.Buscas.Where(b => b.Usuario.Id == userId).ToList();
        }
    }
}
