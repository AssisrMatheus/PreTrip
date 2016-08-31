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
        public void Gravar(Empresa empresa)
        { 
            using (var db = new PreTripDB())
            {
                db.Empresa.Add(empresa);
                db.SaveChanges();
            }
        }        
    }
}