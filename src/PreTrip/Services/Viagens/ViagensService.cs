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

                viagem.Avaliacoes = db.Avaliacoes.Where(a => a.ViagemId == viagem.Id).ToList();
                viagem.Eventos = db.Eventos.Where(e => e.ViagemId == viagem.Id).ToList();

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

        public AvaliacaoMedia GetAvaliacaoMedia(IEnumerable<Avaliacao> avaliacoes)
        {
            //Se não existe nenhuma já retorna
            if(!avaliacoes.Any())
            {
                return new AvaliacaoMedia()
                {
                    Cor = "#fff",
                    Icon = "speaker_notes_off",
                    TextoResultado = "Não existe nenhuma avaliação :("
                };
            }
            
            var total = 0.0;

            avaliacoes
                .Select(a => a.Nota).ToList()//Para cada Nota dentro de uma avaliacao
                .ForEach(n => total+=n);//Soma o total

            //Média é a soma total pela quantidade existente
            var media = total / avaliacoes.Count();

            //Isso tá horrivel ¯\_(ツ)_/¯
            AvaliacaoMedia avaliacao;

            if (media < 10)
            {
                avaliacao = new AvaliacaoMedia()
                {
                    Cor = "#fff",
                    Icon = "sentiment_very_dissatisfied",
                    TextoResultado = "Muito Ruim"
                };
            }                
            else if (media < 25)
            {
                avaliacao = new AvaliacaoMedia()
                {
                    Cor = "#e28400",
                    Icon = "sentiment_dissatisfied",
                    TextoResultado = "Ruim"
                };
            }
            else if (media < 50)
            {
                avaliacao = new AvaliacaoMedia()
                {
                    Cor = "#dcd100",
                    Icon = "sentiment_neutral",
                    TextoResultado = "Neutro"
                };
            }
            else if (media < 90)
            {
                avaliacao = new AvaliacaoMedia()
                {
                    Cor = "#00d480",
                    Icon = "sentiment_satisfied",
                    TextoResultado = "Bom"
                };
            }
            else
            {
                avaliacao = new AvaliacaoMedia()
                {
                    Cor = "#1fbb01",
                    Icon = "sentiment_very_satisfied",
                    TextoResultado = "Excelente!"
                };
            }

            return avaliacao;
        }
    }
}