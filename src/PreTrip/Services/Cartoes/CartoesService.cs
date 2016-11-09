using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreTrip.Services.Cartoes
{
    public class CartaoServiceResult
    {
        public bool Success { get; set; }
        public string Mensagem { get; set; }
    }

    public class CartoesService
    {
        public CartaoServiceResult ValidarCartao(CartaoService.tDadosCartao dadosCartao)
        {
            var result = new CartaoServiceResult()
            {
                Success = true
            };

            //Executa a requisição
            var serviceResult = new CartaoService.Card().ValidarCartao(dadosCartao);

            //Se for autorizado já retorna
            if(serviceResult.ToLowerInvariant() == "AUTORIZADO".ToLowerInvariant())
                return result;
            else
            {
                //Se falhou já seta como falha
                result.Success = false;                

                int errorNumber;
                //Tenta converter o número de erro
                if (int.TryParse(serviceResult, out errorNumber))
                {
                    result.Mensagem = this.GetMensagemPorErro(errorNumber);
                }
                else
                {
                    //Se não foi autorizado e não foi um número de erro, foi algum outro erro
                    //Simplesmente seto a mensagem de erro retornada ao resultado
                    result.Mensagem = serviceResult;
                }

                return result;
            }
        }

        private string GetMensagemPorErro(int errorNumber)
        {
            switch(errorNumber)
            {
                case 1:
                    //Informe o campo <NumeroCartao>;
                    return "O Número do Cartão não foi informado";
                case 2:
                    //Informe o campo <Codigo>
                    return "O Código de Verificação não foi informado";
                case 3:
                    //Informe o campo <NomeCliente>
                    return "O Nome do Cliente não foi informado";
                case 4:
                    //Informe o campo <Validade>
                    return "A Validade do Cartão não foi informada";
                case 5:
                    //Informe o campo <Valor>
                    return "O Valor da compra não foi informado";
                case 6:
                    //Informe o campo <Parcelas>
                    return "O Número de Parcelas não foi informado";
                case 7:
                    //O número do cartão deve ter exatamente 16 caracteres
                    return "O Número do cartão é inválido";
                case 8:
                    //O código verificador deve ter exatamente 3 caracteres
                    return "O Código Verificador informado não é um código válido";
                case 9:
                    //A Validade deve ter exatamente 6 caracteres, sendo eles no formado (yyyymm)
                    return "A Validade é Inválida";
                case 10:
                    //O Cartão de Crédito está vencido, verificar a validade do cartão.
                    return "Cartão de Crédito Vencido, entre em contato com sua operadora";
                case 11:
                    //O valor da compra não ser zero
                    return "O Valor da Compra não pode ser Zero";
                case 12:
                    //O Valor da parcela não pode ser 0 o mínimo é 1. Ou seja, 1 parcela significa A VISTA.
                    return "A Parcela não pode ser zero.";
                case 13:
                    //O Valor da compra ultrapassa o limite atual liberado pelo cartão. 
                    //OBS: O Limite é gerado de forma aleatório no sistema. Ou seja, uma mesma compra pode não passar uma vez, e no outro sim
                    return "Compra NÃO AUTORIZADA. Limite do cartão estourado.";
                case 14:
                    //Informe o campo <NomeEmpresa>
                    return "O Nome da Empresa não foi informado";
                case 15:
                    //Informe o campo <CNPJEmpresa>
                    return "O CNPJ da Empresa não foi informado";
                default:
                    return "Cartão inválido";
            }
        }
    }
}