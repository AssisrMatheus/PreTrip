using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using PreTrip.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreTrip.Services.Enderecos
{
    public class EnderecosService
    {
        private PreTripDB db { get; set; }

        public EnderecosService()
        {
            this.db = new PreTripDB();
        }

        public void Gravar(Endereco endereco)
        {
            db.Enderecos.Add(endereco);
            db.SaveChanges();
        }

        public IEnumerable<Endereco> GetAllByUser(int userId)
        {
            return db.Enderecos.Where(x => x.Usuario.Id == userId).ToList();
        }
    }
}