using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PreTrip.Services.Viagens;

namespace PreTrip.Services.Cidades
{
    public class CidadesService
    {
        public IEnumerable<Cidade> GetAll()
        {
            using (var db = new PreTripDB())
            {
#warning terminar depois.
            }

            return null;
        }

        public IEnumerable<string> GetAllDistinctCity()
        {
            var allViagens = new ViagensService().GetAll();

            var listaCidades = new List<string>();

            listaCidades.AddRange(allViagens.Select(x => x.Origem.Cidade.ToLower()).Distinct());

            return listaCidades;
        }
    }
}