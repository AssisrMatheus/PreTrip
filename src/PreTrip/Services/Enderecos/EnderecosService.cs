using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreTrip.Services.Enderecos
{
    public class EnderecosService
    {
        public void Gravar(Endereco endereco)
        {
            using (var db = new PreTripDB())
            {
                db.Enderecos.Add(endereco);
                db.SaveChanges();
            }
        }       
    }
}