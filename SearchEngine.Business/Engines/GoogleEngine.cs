using Microsoft.Extensions.Options;
using SearchEngine.Business.Interfaces;
using SearchEngine.Business.Responses;
using SearchEngine.Models.Settings;
using System.Threading.Tasks;

namespace SearchEngine.Business.Engines
{
    public class GoogleEngine : BaseEngine
    {
        public GoogleEngine(IOptions<GoogleSettings> settings)
        {
            Settings = settings.Value;
        }

        protected override IResponse ParseResponse(string response)
        {
            return Parser.ParseJsonResponse<GoogleResponse>(response);
        }
    }
}
