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
        public void InserirBusca(Busca busca)
        {
            using (var db = new PreTripDB())
            {
                if (AlgumCampoPreenchido(busca))
                {
                    db.Buscas.Add(busca);
                    db.SaveChanges();
                }
            }
        }

        private bool AlgumCampoPreenchido(Busca busca)
        {
            if (!string.IsNullOrEmpty(busca.Titulo) 
                || !string.IsNullOrEmpty(busca.Origem) 
                || !string.IsNullOrEmpty(busca.Destino)
                || busca.Preco!= 0 || busca.QuantidadeLugares != 0 
                || busca.LugaresDisponiveis != 0)
            {
                return true;
            }

            return false;
        }
        public IEnumerable<Viagem> GetAllFilter(Busca filtros)
        {                     
            using (var db = new PreTripDB())
            {
                var viagens = (from viag in db.Viagens.ToList()
                               join destino in db.Enderecos on viag.Destino.Id equals destino.Id
                               join origem in db.Enderecos on viag.Origem.Id equals origem.Id
                               join emp in db.Empresas on viag.Empresa.Id equals emp.Id
                               join veic in db.Veiculos on viag.Veiculo.Id equals veic.Id                             

                               where
                               //se o filtro estiver nulo, ele busca o que esta no banco,exemplo, se filtro origem estiver nulo, para cada linha ira buscar origem = origem do banco
                               origem.Cidade
                               .ToUpper()
                               .Contains((string.IsNullOrEmpty(filtros.Origem) ? origem.Cidade.ToUpper() : filtros.Origem.ToUpper()))

                               && destino.Cidade
                               .ToUpper()
                               .Contains((string.IsNullOrEmpty(filtros.Destino) ? destino.Cidade.ToUpper() : filtros.Destino.ToUpper()))

                               && viag.Titulo
                               .ToUpper()
                               .Contains((string.IsNullOrEmpty(filtros.Titulo) ? viag.Titulo.ToUpper() : filtros.Titulo.ToUpper()))

                               && viag.PrecoPassagem == (NumeroNaoPreenchido(filtros.Preco) ? viag.PrecoPassagem : filtros.Preco)
                               && viag.QuantidadeLugaresDisponiveis == (NumeroNaoPreenchido(filtros.QuantidadeLugares) ? viag.QuantidadeLugaresDisponiveis : filtros.QuantidadeLugares)

                               select new Viagem()
                               {
                                   Id = viag.Id,
                                   Titulo = viag.Titulo,
                                   Descricao = viag.Descricao,
                                   Destino = destino,
                                   Origem = origem,
                                   Empresa = emp,
                                   Veiculo = veic,
                                   DtHrChegadaEstimada = viag.DtHrChegadaEstimada,
                                   DtHrSaida = viag.DtHrSaida,
                                   PrecoPassagem = viag.PrecoPassagem,
                                   QuantidadeLugaresDisponiveis = viag.QuantidadeLugaresDisponiveis,
                                   UrlImagem = viag.UrlImagem,                                   
                               }).ToList();                

                 return viagens;
            }
        }
                
        private bool NumeroNaoPreenchido(Object numero)
        {
            if (Convert.ToDouble(numero) == 0)
            {
                return true;
            }
            return false;
        }  

        public IEnumerable<Viagem> GetAll()
        {
            using (var db = new PreTripDB())
            {
                return (from viag in db.Viagens.ToList()
                        join dest in db.Enderecos on viag.Destino.Id equals dest.Id
                        join orig in db.Enderecos on viag.Origem.Id equals orig.Id
                        join emp in db.Empresas on viag.Empresa.Id equals emp.Id
                        join veic in db.Veiculos on viag.Veiculo.Id equals veic.Id                        
                      
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
                        }).ToList();
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

                viagem.Avaliacoes = (from aval in db.Avaliacoes.ToList()
                                     join usua in db.Usuarios on aval.Usuario.Id equals usua.Id
                                     join pess in db.Pessoas on usua.Pessoa.Id equals pess.Id
                                     where aval.ViagemId == viagem.Id
                                     select new Avaliacao()//Pego tudo da avaliacao
                                     {
                                         Comentario = aval.Comentario,
                                         Nota = aval.Nota,
                                         Usuario = new Usuario() //Somente o que preciso do usuario(Não preciso do login e senha dele por exemplo)
                                         { 
                                             Pessoa = pess,
                                             Id = usua.Id,
                                             Email = usua.Email
                                         },
                                         ViagemId = aval.ViagemId,
                                         Viagem = aval.Viagem,
                                         Id = aval.Id
                                     }).ToList();

                viagem.Eventos = db.Eventos.Where(e => e.ViagemId == viagem.Id).ToList();

                return viagem;                    
            }
        }

        public void Inserir(Viagem viagem)
        {
            using (var db = new PreTripDB())
            {               
                if(PreTripSession.Usuario != null){
                    int idUsuario = PreTripSession.Usuario.Id;

                    viagem.Destino.UsuarioId = idUsuario;
                    viagem.Origem.UsuarioId = idUsuario;

                    db.Viagens.Add(viagem);
                    db.SaveChanges();
                }             
            }
        }

        public void InserirAvaliacao(Avaliacao avaliacao)
        {
            if (avaliacao.ViagemId == 0) throw new ArgumentNullException("Id viagem não pode ser nulo ou 0");
            if (avaliacao.Usuario == null) throw new ArgumentNullException("Avaliacao precisa ter um usuário!");

            using (var db = new PreTripDB())
            {
                db.Avaliacoes.Add(avaliacao);
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
                    Cor = "#000",
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