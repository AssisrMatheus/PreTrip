using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using PreTrip.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreTrip.Services
{
    public class UsuarioService
    {
        public Usuario GetWithLoginPass(string login, string password)
        {
            using (var db = new PreTripDB())
            {
                var senhaHash = CreateMD5.GetHash(password);
                return db.Usuario
                    .FirstOrDefault(u => u.Login == login && u.Senha == senhaHash);
            }
        }
    }
}
