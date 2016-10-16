using PreTrip.Lib.Utils;
using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreTrip.Services.Empresas
{
    public class EmpresasService
    {
        private PreTripDB db { get; set; }

        public EmpresasService()
        {
            this.db = new PreTripDB();
        }

        public IEnumerable<Empresa> GetAll(int userId)
        {
            return db.Empresas.Where(e => e.Usuario.Id == userId).ToList();
        }

        public void Inserir(Empresa empresa)
        {
            db.Empresas.Add(empresa);
            db.SaveChanges();
        }
    }
}