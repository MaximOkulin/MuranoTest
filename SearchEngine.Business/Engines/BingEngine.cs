using SearchEngine.Business.Responses;
using System.Threading.Tasks;
using PublicResXFileCodeGenerator;
using SearchEngine.Business.Interfaces;
using Microsoft.Extensions.Options;
using System.Threading;
using SearchEngine.Business.Settings;
using SearchEngine.Business.Extensions;
using System;
using System.Net.Http;

namespace SearchEngine.Business.Engines
{
    public class BingEngine : BaseEngine
    {
        public BingEngine(IOptions<BingSettings> settings, HttpClient httpClient) : base(httpClient)
        {
            Settings = settings.Value;
        }

        protected override async Task<string> ExecuteRequest(string requestString, CancellationToken token)
        {
            HttpClient.DefaultRequestHeaders.Add(StringResources.OcpApimSubscriptionKey, Settings.Value);

            return await base.ExecuteRequest(requestString, token);
        }

        protected override string PrepareRequest(string query)
        {
            query = Uri.EscapeDataString(query.ReplaceWhiteSpaceToPlusSymbol());
            string request = string.Format(Settings.RequestFormat, query);

            return request;
        }

        protected override IResponse ParseResponse(string response)
        {
            return Parser.ParseJsonResponse<BingResponse>(response);
        }
    }
}
