using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using PreTrip.Session;
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

        public IEnumerable<Endereco> GetAllByUser()
        {
            if (PreTripSession.Usuario != null) {
                int idUsuario = PreTripSession.Usuario.Id;
                using (var db = new PreTripDB())
                {
                    return (from end in db.Enderecos.ToList()

                            where end.UsuarioId == idUsuario

                            select new Endereco()
                            {
                                Cidade = end.Cidade,
                                Bairro = end.Bairro,
                                Estado = end.Estado,
                                Complemento = end.Complemento,
                                Numero = end.Numero

                            }).ToList();
                }  
            }

            return null;
        }
    }
}