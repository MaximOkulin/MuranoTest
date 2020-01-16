using Microsoft.Extensions.Options;
using PublicResXFileCodeGenerator;
using SearchEngine.Business.Engines;
using SearchEngine.Business.Interfaces;
using SearchEngine.Business.Responses;
using SearchEngine.Models.Settings;
using SearchEngine.Tests.Helpers;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace SearchEngine.Tests
{
    public class YandexEngineTests
    {
        private class YandexEngineChild : YandexEngine
        {
            public YandexEngineChild(IOptions<YandexSettings> settings) : base(settings)
            {

            }

            public async Task<IResponse> ParseReponse(string response)
            {
                return await base.ParseResponse(response);
            }
        }

        [Fact]
        public void ParseReponse_Should_BeEquivalentTo()
        {
            IOptions<YandexSettings> options = Options.Create<YandexSettings>(new YandexSettings());
            var yandexEngine = new YandexEngineChild(options);

            yandexsearch expected = TestHelper.GetTestYandexSearch();

            var actual = yandexEngine.ParseReponse(StringResources.YandexTestXml).Result;

            var act = actual as yandexsearch;

            expected.Should()
                .BeEquivalentTo(act, options => options.Including(r => r.response.results.grouping.group).Excluding(o => o.Name));
        }
    }
}
