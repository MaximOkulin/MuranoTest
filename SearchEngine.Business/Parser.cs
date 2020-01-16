using SearchEngine.Business.Interfaces;
using System.Text.Json;

namespace SearchEngine.Business
{
    public class Parser
    {
        public static IResponse ParseJsonResponse<T>(string response)  where T : IResponse
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
                    return (IResponse)pkg;
                }
                catch
                {

                }
            }

            return null;
        }
    }
}
