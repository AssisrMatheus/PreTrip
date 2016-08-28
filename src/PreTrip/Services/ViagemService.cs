using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreTrip.Services
{
    public class ViagemService
    {
        public IEnumerable<Viagem> GetAll()
        {
            using (var db = new PreTripDB())
            {
                return db.Viagem.ToList();
            }
        }
    }
}