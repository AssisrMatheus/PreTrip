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

        public Pessoa GetPessoaById(int id)
        {
            using (var db = new PreTripDB())
            {
                var pessoaRetornada = (from pessoa in db.Pessoas.ToList()
                                       join conta in db.ContasBancarias on pessoa.ContaBancariaId equals conta.Id
                                       where pessoa.Id == id

                                       select new Pessoa()
                                       {
                                           ContaBancaria = conta,
                                           Id = pessoa.Id,
                                           Nome = pessoa.Nome

                                       }).FirstOrDefault();

                return pessoaRetornada;
            }
        }

        public void SalvarModificacoesPessoa(Pessoa pessoa)
        {
            using (var db = new PreTripDB())
            {       
                db.ContasBancarias.AddOrUpdate(pessoa.ContaBancaria);
                db.SaveChanges();
            }
        }

        public void SalvarModificacoes(Usuario usuario)
        {
            using (var db = new PreTripDB())
            {
                
                //db.ContasBancarias.Add(usuario.Pessoa.Conta);
                //db.SaveChanges();

                //db.Pessoas.AddOrUpdate(usuario.Pessoa);        

                db.Pessoas.AddOrUpdate(usuario.Pessoa);
                db.SaveChanges();
            }
        }

        public IEnumerable<Usuario> GetUsers(Func<Usuario, bool> filtro = null)
        {
            using (var db = new PreTripDB())
            {
                //Pega um usuário com seu objeto pessoa preenchido
                var usuarios = from usu in db.Usuarios.ToList()
                               join pes in db.Pessoas.ToList()
                               on usu.PessoaId equals pes.Id
                               join cont in db.ContasBancarias.ToList()
                               on pes.ContaBancariaId equals cont.Id
                               select new Usuario()//Aqui seto os parâmetros que virão no select(quais colunas)
                               {
                                   IsAdmin = usu.IsAdmin,
                                   Pessoa = new Pessoa()
                                   {
                                       Id = pes.Id,
                                       Cpf = pes.Cpf,
                                       DtNascimento = pes.DtNascimento,
                                       Nome = pes.Nome,
                                       Telefone = pes.Telefone,
                                       UrlImagem = pes.UrlImagem,
                                       ContaBancariaId = cont.Id,
                                       ContaBancaria = cont,
                                       Pedidos = pes.Pedidos
                                   },
                                   Email = usu.Email,
                                   Login = usu.Login,
                                   Id = usu.Id,
                                   Senha = usu.Senha
                               };

                if (filtro != null && usuarios.Any())
                    return usuarios.Where(filtro);
                else
                    return usuarios;
            }
        }

        public IEnumerable<Model.Classes.Interesse> GetUsuarioInteresses(Func<Usuario, bool> filtro = null)
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
            if (this.ValidaNovoUsuario(usuario)) { 
                using (var db = new PreTripDB())
                {
                    //Converte para md5
                    usuario.Senha = CreateMD5.GetHash(usuario.Senha);

                    db.Usuarios.Add(usuario);
                    db.SaveChanges();
                }
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
