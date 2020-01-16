using System;
using System.Net.Http;
using System.Threading.Tasks;
using PublicResXFileCodeGenerator;
using SearchEngine.Business.Interfaces;
using SearchEngine.Business.Extensions;
using SearchEngine.Models.Interfaces;
using System.Threading;

namespace SearchEngine.Business.Engines
{
    public abstract class BaseEngine : IEngine
    {
        protected static readonly HttpClient HttpClient = new HttpClient();
        public ISettings Settings { get; set; }
        public string EngineName { 
            get
            {
                return GetType().Name.Replace(StringResources.Engine, string.Empty);
            }
        }

        public Task<IResponse> Execute(string query, CancellationToken token)
        {
            Task<IResponse> result = null;
            try
            {
                var requestString = PrepareRequest(query);
                var response = ExecuteRequest(requestString, token);
                if (response != null)
                {
                    result = response.ContinueWith(p => ParseResponse(p.Result)).Result;
                }                
            }
            catch(Exception)
            {
                result = null;
            }
            return result;
        }

        protected virtual string PrepareRequest(string query)
        {
            string request = string.Format(Settings.RequestFormat, Settings.Value, query.ReplaceWhiteSpaceToPlusSymbol());

            return request;
        }
        
        protected abstract Task<IResponse> ParseResponse(string response);


        protected virtual async Task<string> ExecuteRequest(string requestString, CancellationToken token)
        {
            string result = null;
            try
            {
                var response = await HttpClient.GetAsync(requestString, token);
                result = await response.Content.ReadAsStringAsync();
            }
            catch(HttpRequestException)
            {
                result = null;
            }

            return result;
        }     
    }
}
