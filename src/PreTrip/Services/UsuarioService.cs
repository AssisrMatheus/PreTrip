using PreTrip.Lib.Utils;
using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreTrip.Services
{
    public class UsuarioService
    {
        public IEnumerable<Usuario> GetAll()
        {
            using (var db = new PreTripDB())
            {
                return db.Usuario.ToList();
            }
        }

        public Usuario GetWithLoginPass(string login, string password)
        {
            //Precisamos testar se o usuário é admin, se for, não podemos usar a senha para buscar do banco
            using (var db = new PreTripDB())
            {
                //Pegamos somente por login e somente se for admin.
                var usuAdmin = db.Usuario.ToList() //Converte para lista para materializar e ser link to object e poder usar "new usuario" em baixo
                    .Join(db.Pessoa, //Join na tabela pessoa
                        usu => usu.Pessoa, //Onde o usuario.pessoa
                        pes => pes, //precisa ser a pessoa da tabela pessoa
                        (usu, pes) => new Usuario() //preenche o usuario com os novos parametros
                        {
                            IsAdmin = usu.IsAdmin,
                            Pessoa = pes,
                            Email = usu.Email,
                            Login = usu.Login,
                            Id = usu.Id,
                            Pedidos = usu.Pedidos,
                            Senha = usu.Senha
                        }
                     )
                    .FirstOrDefault(u => u.Login == login && u.IsAdmin);

                //Se existe um usuario admin com o login
                if (usuAdmin != null)
                {
                    //A senha do admin é a senha gerada, se for diferente da digitada
                    //ele não é um usuario válido, retorne nulo.
                    if (usuAdmin.Senha != password)
                        return null;
                    else
                        //Se a senha digitada foi correta com a gerada é um login de admin válido
                        return usuAdmin;
                }
                else
                {
                    //Se não encontrou nenhum usuario admin com aquele login, é um usuário normal
                    //então precisa ter digitado a senha correta também, e retornará nulo se inválido
                    var senhaHash = CreateMD5.GetHash(password);
                    return db.Usuario.ToList() //Converte para lista para materializar e ser link to object e poder usar "new usuario" em baixo
                        .Join(db.Pessoa, //Join na tabela pessoa
                            usu => usu.Pessoa, //Onde o usuario.pessoa
                            pes => pes, //precisa ser a pessoa da tabela pessoa
                            (usu, pes) => new Usuario() //preenche o usuario com os novos parametros
                            {
                                IsAdmin = usu.IsAdmin,
                                Pessoa = pes,
                                Email = usu.Email,
                                Login = usu.Login,
                                Id = usu.Id,
                                Pedidos = usu.Pedidos,
                                Senha = usu.Senha
                            }
                         )
                         .FirstOrDefault(u => u.Login == login && u.Senha == senhaHash);
                }
            }
        }

        /// <summary>
        /// Grava o usuário no banco
        /// </summary>
        /// <param name="usuario"></param>
        public void Gravar(Usuario usuario)
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
            var usuBanco = this.GetWithLoginPass(usuario.Login, usuario.Senha);

            //Se o usuário existir não podemos deixar registrar
            if (usuBanco != null)
                return false;

            return true;
        }
    }
}
