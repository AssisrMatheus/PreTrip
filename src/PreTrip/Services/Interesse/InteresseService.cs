using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using PreTrip.Model.Context;
using PreTrip.Model.Classes;
using PreTrip.Services.Viagens;

namespace PreTrip.Services.Interesse
{
    public class InteresseService
    {
        private PreTripDB db { get; set; }

        public InteresseService()
        {
            this.db = new PreTripDB();
        }

        public void InsertOrUpdate(IEnumerable<Model.Classes.Interesse> interesses)
        {
            interesses.ToList().ForEach(x =>
            {
                db.Interesses.AddOrUpdate(x);
            });

            db.SaveChanges();
        }

        public IEnumerable<string> GetAllCities()
        {
            var allViagens = new ViagensService().GetViagens();

            var listaCidades = new List<string>();

            listaCidades.AddRange(allViagens.Select(x => x.Origem.Cidade).Distinct());

            return listaCidades;
        }
    }
}