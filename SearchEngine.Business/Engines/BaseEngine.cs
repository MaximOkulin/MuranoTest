using System;
using System.Net.Http;
using System.Threading.Tasks;
using PublicResXFileCodeGenerator;
using SearchEngine.Business.Interfaces;
using SearchEngine.Business.Extensions;
using System.Threading;

namespace SearchEngine.Business.Engines
{
    /// <summary>
    /// Base engine ensures and describes main logic for all search engines
    /// </summary>
    public abstract class BaseEngine : IEngine
    {
        protected HttpClient HttpClient = new HttpClient();
        public ISettings Settings { get; set; }
        public string EngineName { 
            get
            {
                return GetType().Name.Replace(StringResources.Engine, string.Empty);
            }
        }

        /// <summary>
        /// Executes search
        /// </summary>
        /// <param name="query"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<IResponse> ExecuteAsync(string query, CancellationToken token)
        {
            Task<IResponse> result = null;
            try
            {
                var requestString = PrepareRequest(query);
                var response = ExecuteRequest(requestString, token).Result;
                if (response != null)
                {
                    result = Task.FromResult(ParseResponse(response));
                }               
            }
            catch(Exception)
            {
                result = null;
            }
            return result;
        }

        /// <summary>
        /// Prepares request
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        protected virtual string PrepareRequest(string query)
        {
            query = Uri.EscapeDataString(query.ReplaceWhiteSpaceToPlusSymbol());
            string request = string.Format(Settings.RequestFormat, Settings.Value, query);

            return request;
        }
        
        protected abstract IResponse ParseResponse(string response);

        /// <summary>
        /// Executes HTTP GET-request (inherited class can implement own method, ex. HTTP POST-request)
        /// </summary>
        /// <param name="requestString"></param>
        /// <param name="token">CancellationToken</param>
        /// <returns></returns>

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
