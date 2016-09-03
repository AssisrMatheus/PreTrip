﻿using PreTrip.Model.Classes;
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
                return from viag in db.Viagens.ToList()
                       join dest in db.Enderecos.ToList()
                       on viag.Destino equals dest
                       select new Viagem()
                       {
                           Id = viag.Id,
                           PrecoPassagem = viag.PrecoPassagem,
                           Descricao = viag.Descricao,
                           Destino = dest
                       };
            }
        }

        public Viagem GetViagem(int viagemId)
        {
            using (var db = new PreTripDB())
            {
                var viagem = (from viag in db.Viagens.ToList()
                             join dest in db.Enderecos on viag.Destino.Id equals dest.Id
                             join orig in db.Enderecos on viag.Origem.Id equals orig.Id
                             join emp in db.Empresas on viag.Empresa.Id equals emp.Id
                             join veic in db.Veiculos on viag.Veiculo.Id equals veic.Id
                             where viag.Id == viagemId
                             select new Viagem()
                             {
                                 Id = viag.Id,
                                 Titulo = viag.Titulo,
                                 Descricao = viag.Descricao,
                                 Destino = dest,
                                 Origem = orig,
                                 Empresa = emp,
                                 Veiculo = veic,
                                 DtHrChegadaEstimada = viag.DtHrChegadaEstimada,
                                 DtHrSaida = viag.DtHrSaida,
                                 PrecoPassagem = viag.PrecoPassagem,
                                 QuantidadeLugaresDisponiveis = viag.QuantidadeLugaresDisponiveis,
                                 UrlImagem = viag.UrlImagem
                             }).FirstOrDefault();

                viagem.Avaliacoes = db.Avaliacoes.Where(a => a.ViagemId == viagem.Id);
                viagem.Eventos = db.Eventos.Where(e => e.ViagemId == viagem.Id);

                return viagem;                   
            }
        }

        public void Inserir(Viagem viagem)
        {
            using (var db = new PreTripDB())
            {
                db.Viagens.Add(viagem);
                db.SaveChanges();
            }
        }
    }
}