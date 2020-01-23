using Microsoft.Extensions.Options;
using PublicResXFileCodeGenerator;
using SearchEngine.Business.Engines;
using SearchEngine.Business.Interfaces;
using SearchEngine.Business.Responses;
using SearchEngine.Tests.Helpers;
using FluentAssertions;
using Xunit;
using SearchEngine.Business.Settings;
using RichardSzalay.MockHttp;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

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

            public void SetupHttpClientWithMock(MockHttpMessageHandler mockHttp)
            {
                HttpClient = new HttpClient(mockHttp);
            }
        }

        [Fact]
        public async Task<int> ExecuteAsync_GetGoodResponse()
        {
            // arrange
            IOptions<YandexSettings> options = Options.Create<YandexSettings>(new YandexSettings
            {
                Name = StringResources.Yandex,
                RequestFormat = "https://yandex.com/search/xml?l10n=en&{0}&query={1}",
                Value = "user=maxim-okulin&key=03.271131870:3cd406dd968f16a13abeb89e0af29889"
            });

            var yandexEngine = new YandexEngineChild(options);
            string query = TestStringResources.YandexGroupOfEightQuery;

            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(TestStringResources.YandexGroupOfEightTestRequest).Respond(StringResources.MIME_Xml, TestStringResources.YandexGroupOfEightTestXml);

            yandexEngine.SetupHttpClientWithMock(mockHttp);

            yandexsearch expected = TestHelper.GetTestGroupOfEightSearch();

            // act
            var response = await yandexEngine.ExecuteAsync(query, CancellationToken.None);

            // assert
            response.Should().BeEquivalentTo(expected, options => options.Including(r => r.response.results.grouping.group).Excluding(o => o.Name));

            return 0;
        }


        [Fact]
        public void ParseResponse_Should_BeEquivalentTo_With_5Items()
        {
            // arrange
            IOptions<YandexSettings> options = Options.Create<YandexSettings>(new YandexSettings());
            var yandexEngine = new YandexEngineChild(options);

            yandexsearch expected = TestHelper.GetTestKazanKremlinSearch();

            // act
            var actual = yandexEngine.ParseResponse(TestStringResources.YandexTestXml);

            var act = actual as yandexsearch;

            // assert
            act.Should()
                .BeEquivalentTo(expected, options => options.Including(r => r.response.results.grouping.group).Excluding(o => o.Name));
        }
    }
}
