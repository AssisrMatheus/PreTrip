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
        public IEnumerable<Empresa> GetAll(int userId)
        {
            using (var db = new PreTripDB())
            {
                return db.Empresas.Where(e => e.Usuario.Id == userId).ToList();
            }
        }

        public void Inserir(Empresa empresa)
        {
            using (var db = new PreTripDB())
            {
                db.Empresas.Add(empresa);
                db.SaveChanges();
            }
        }
    }
}