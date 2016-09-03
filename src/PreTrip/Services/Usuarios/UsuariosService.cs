﻿using PreTrip.Lib.Utils;
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
            //Se é um usuário válido, cadastra.
            if (this.ValidaNovoUsuario(usuario))
                using (var db = new PreTripDB())
                {
                    //Converte para md5
                    usuario.Senha = CreateMD5.GetHash(usuario.Senha);

                    db.Usuario.Add(usuario);
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

                    db.Usuario.AddOrUpdate(usuario);
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
    }
}
