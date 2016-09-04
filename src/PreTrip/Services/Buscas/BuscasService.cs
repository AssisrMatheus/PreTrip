using PreTrip.Lib.Utils;
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
                if (ValidationsUtils.AlgumCampoPreenchido(busca))
                {
                    db.Buscas.Add(busca);
                    db.SaveChanges();
                }               
            }
        }       
    }
}