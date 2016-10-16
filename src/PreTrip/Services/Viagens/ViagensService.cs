using PreTrip.Model.Classes;
using PreTrip.Model.Context;
using PreTrip.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace PreTrip.Services.Viagens
{
    public class ViagensService
    {
        private PreTripDB db { get; set; }

        public ViagensService()
        {
            this.db = new PreTripDB();
        }

        public IEnumerable<Viagem> GetAll()
        {
            return db.Viagens.ToList();
        }

        public IEnumerable<Viagem> GetAllFilter(Busca filtros)
        {
            //Cria a query inicial
            var viagens = db.Viagens;

            //Vai aplicando os filtros where na query de acordo com o necessário

            if (!string.IsNullOrEmpty(filtros.Origem))
                viagens.Where(x => x.Origem.Cidade.ToUpper().Contains(filtros.Origem.ToUpper()));

            if (!string.IsNullOrEmpty(filtros.Destino))
                viagens.Where(x => x.Destino.Cidade.ToUpper().Contains(filtros.Destino.ToUpper()));

            if (!string.IsNullOrEmpty(filtros.Titulo))
                viagens.Where(x => x.Titulo.ToUpper().Contains(filtros.Titulo.ToUpper()));

            if (filtros.Preco != 0)
                viagens.Where(x => x.PrecoPassagem <= filtros.Preco);

            if (filtros.QuantidadeLugares != 0)
                viagens.Where(x => x.QuantidadeLugaresDisponiveis >= filtros.QuantidadeLugares);

            //Somente depois de criar a query com os filtros materializa o resultado com tolist, trazendo somente o necessário do filtro(entity s2)
            return viagens.ToList();

        }

        public IEnumerable<Viagem> GetAllFromPessoa(int pessoaId)
        {
            return db.Viagens.Where(x => x.Pessoa.Id == pessoaId).ToList();
        }

        public Viagem GetViagem(int viagemId)
        {
            return db.Viagens
                .Where(x => x.Id == viagemId).FirstOrDefault();
        }

        public void Gravar(Viagem viagem)
        {
            if (viagem.Pessoa == null || viagem.Pessoa.Id == 0) throw new ArgumentNullException("Viagem precisa ter uma pessoa válida");

            //Busca pelo id
            var viagExistente = db.Viagens.Where(x => x.Id == viagem.Id).FirstOrDefault();

            //Se existe
            if (viagExistente != null)
            {
                db.Entry(viagExistente).CurrentValues.SetValues(viagem);
            }
            else
            {
                db.Viagens.Add(viagem);
            }

            db.SaveChanges();

        }

        public void InserirAvaliacao(Avaliacao avaliacao)
        {
            if (avaliacao.Viagem == null || avaliacao.Viagem.Id == 0) throw new ArgumentNullException("Avaliação precisa ter uma viagem válida");
            if (avaliacao.Usuario == null || avaliacao.Usuario.Id == 0) throw new ArgumentNullException("Avaliacao precisa ter um usuário válido");


            db.Avaliacoes.Add(avaliacao);
            db.SaveChanges();
        }

        public void InserirBusca(Busca busca)
        {
            if (!string.IsNullOrEmpty(busca.Titulo)
            || !string.IsNullOrEmpty(busca.Origem)
            || !string.IsNullOrEmpty(busca.Destino)
            || busca.Preco != 0 || busca.QuantidadeLugares != 0
            || busca.LugaresDisponiveis != 0)
            {
                db.Buscas.Add(busca);
                db.SaveChanges();
            }
        }

        public AvaliacaoMedia GetAvaliacaoMedia(IEnumerable<Avaliacao> avaliacoes)
        {
            //Se não existe nenhuma já retorna
            if (!avaliacoes.Any())
            {
                return new AvaliacaoMedia()
                {
                    Cor = "#000",
                    Icon = "speaker_notes_off",
                    TextoResultado = "Não existe nenhuma avaliação :("
                };
            }

            var total = 0.0;

            avaliacoes
                .Select(a => a.Nota).ToList()//Para cada Nota dentro de uma avaliacao
                .ForEach(n => total += n);//Soma o total

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