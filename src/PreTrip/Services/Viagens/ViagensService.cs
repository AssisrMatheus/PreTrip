using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreTrip.Services.Viagens
{
    public class ViagensService
    {
        public IEnumerable<Viagem> GetAll()
        {
            using (var db = new PreTripDB())
            {
                return db.Viagem.ToList();
            }
        }

        public void Inserir(Viagem viagem)
        {
            using (var db = new PreTripDB())
            {
                db.Viagem.Add(viagem);
                db.SaveChanges();
            }
        }
    }
}