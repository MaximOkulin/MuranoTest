using SearchEngine.Business.Responses;
using SearchEngine.Models;
using System.Threading.Tasks;
using System.Linq;
using PublicResXFileCodeGenerator;
using SearchEngine.Business.Interfaces;
using Microsoft.Extensions.Options;
using SearchEngine.Models.Settings;
using System.Threading;

namespace SearchEngine.Business.Engines
{
    public class BingEngine : BaseEngine
    {
        public BingEngine(IOptions<BingSettings> settings)
        {
            Settings = settings.Value;
        }

        protected override async Task<string> ExecuteRequest(string requestString, CancellationToken token)
        {
            HttpClient.DefaultRequestHeaders.Add(StringResources.OcpApimSubscriptionKey, Settings.Value);

            return await base.ExecuteRequest(requestString, token);
        }

        protected override async Task<IResponse> ParseResponse(string response)
        {
            var task = Parser.ParseJsonResponse<BingResponse>(response);
            if (task != null)
            {
                return await task;
            }
           
            return null;
        }
    }
}
