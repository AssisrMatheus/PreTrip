using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PreTrip.Lib.Utils
{
    public class RestService
    {
        /// <summary>
        /// Propriedade do WEB Config com o link do webService. Ex: http://localhost:4131/
        /// </summary>
        private static string ServiceURL
        {
            get
            {
                var url = ConfigurationManager.AppSettings["ServiceURL"];

                if (string.IsNullOrEmpty(url))
                    throw new Exception("Chave em appSettings[ServiceURL] vazia ou não encontrada no .config");

                return url;
            }
        }

        public static T Call<T>(string method, dynamic parameters)
        {
            //Crio o cliente
            var client = new RestClient(ServiceURL);

            //Seto o tipo pra post e falo qual será a api a ser acessada pelo method
            var request = new RestRequest(method, Method.POST);

            //Adiciono o que foi entregue aos parâmetros em json
            request.AddJsonBody(parameters);

            //Executo e pego o resultado
            IRestResponse response = client.Execute(request);

            var content = response.Content;

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(content);

            //Converto o resultado em string pra Jobject, e converto para o objeto do tipo fornecido pelo T
            return JObject.Parse(content).ToObject<T>();
        }
    }
}
