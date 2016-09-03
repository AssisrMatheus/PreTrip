using PreTrip.Model.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreTrip.ViewModel
{
    public class ViagensViewModel
    {
        public IEnumerable<Viagem> Viagens { get; set; }

        public string HeaderViagens { get; set; }

        public Busca BuscaViagens { get; set; }

        //Essa viagem pode ser usada tanto para alterar quanto para criar, depende de que view estamos
        public Viagem Viagem { get; set; }
    }
}