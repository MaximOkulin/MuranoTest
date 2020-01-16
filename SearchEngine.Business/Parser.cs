using SearchEngine.Business.Interfaces;
using System.Text.Json;

namespace SearchEngine.Business
{
    public class Parser
    {
        /// <summary>
        /// Deserializes json to object, which implements IResponse-interface
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        public static IResponse ParseJsonResponse<T>(string response)  where T : IResponse
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            IResponse result = null;
            if (response != null)
            {
                try
                {
                    result = JsonSerializer.Deserialize<T>(response, options);
                }
                catch
                {
                    result = null;
                }
            }

            return result;
        }
    }
}
