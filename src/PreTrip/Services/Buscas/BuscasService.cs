using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreTrip.Services.Buscas
{
    public class BuscasService
    {
        public void Inserir(Busca busca)
        {
            using (var db = new PreTripDB())
            {
                db.Buscas.Add(busca);
                db.SaveChanges();
            }
        }
    }
}