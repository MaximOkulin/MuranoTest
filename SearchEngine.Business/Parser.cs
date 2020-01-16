using SearchEngine.Business.Interfaces;
using System.Text.Json;
using System.Threading.Tasks;

namespace SearchEngine.Business
{
    public class Parser
    {
        public static Task<IResponse> ParseJsonResponse<T>(string response)  where T : IResponse
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (response != null)
            {
                try
                {
                    var pkg = JsonSerializer.Deserialize<T>(response, options);
                    return Task.FromResult((IResponse)pkg);
                }
                catch
                {

                }
            }

            return null;
        }
    }
}
