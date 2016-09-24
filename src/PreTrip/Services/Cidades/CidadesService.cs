using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreTrip.Services.Cidades
{
    public class CidadesService
    {
        public IEnumerable<Cidade> GetAll()
        {
            using (var db = new PreTripDB())
            {
                return db.Cidades.ToList();
            }
        }
    }   
}