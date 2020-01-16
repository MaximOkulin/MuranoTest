using Microsoft.Extensions.Options;
using PublicResXFileCodeGenerator;
using SearchEngine.Business.Engines;
using SearchEngine.Business.Interfaces;
using SearchEngine.Business.Responses;
using SearchEngine.Tests.Helpers;
using FluentAssertions;
using Xunit;
using SearchEngine.Business.Settings;

namespace SearchEngine.Tests
{
    public class YandexEngineTests
    {
        private class YandexEngineChild : YandexEngine
        {
            public YandexEngineChild(IOptions<YandexSettings> settings) : base(settings)
            {

            }

            public new IResponse ParseResponse(string response)
            {
                return base.ParseResponse(response);
            }
        }

        [Fact]
        public void ParseResponse_Should_BeEquivalentTo_With_5Items()
        {
            // arrange
            IOptions<YandexSettings> options = Options.Create<YandexSettings>(new YandexSettings());
            var yandexEngine = new YandexEngineChild(options);

            yandexsearch expected = TestHelper.GetTestYandexSearch();

            // act
            var actual = yandexEngine.ParseResponse(StringResources.YandexTestXml);

            var act = actual as yandexsearch;

            // assert
            expected.Should()
                .BeEquivalentTo(act, options => options.Including(r => r.response.results.grouping.group).Excluding(o => o.Name));
        }
    }
}
