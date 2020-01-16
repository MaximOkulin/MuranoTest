using SearchEngine.Business.Engines;
using Xunit;
using System.Threading.Tasks;
using SearchEngine.Business.Interfaces;
using SearchEngine.Business.Responses;
using PublicResXFileCodeGenerator;
using Microsoft.Extensions.Options;
using SearchEngine.Models.Settings;
using FluentAssertions;

namespace SearchEngine.Tests
{
    
    public class BingEngineTests
    {
        private class BingEngineChild : BingEngine
        {
            public BingEngineChild(IOptions<BingSettings> settings) : base(settings)
            {
                
            }

            public async Task<IResponse> ParseReponse(string response)
            {
                return await base.ParseResponse(response);
            }
        }

        [Fact]
        public void ParseResponse_Should_BeEquivalentTo_With_3Items()
        {
            IOptions<BingSettings> someOptions = Options.Create<BingSettings>(new BingSettings());
            var bingEngine = new BingEngineChild(someOptions);


            BingResponse expected = new BingResponse()
            {
                webPages = new Webpages()
                {
                    value = new Value[] { new Value { name = "Bill & Melinda Gates Foundation - Official Site", url = "http://www.gatesfoundation.org/" },
                                          new Value { name = "Bill Gates - Wikipedia, the free encyclopedia", url = "https://en.wikipedia.org/wiki/Bill_Gates" },
                                          new Value { name = "Bill Gates - Official Site", url = "https://www.gatesnotes.com/" }
                    }
                }
            };

            var actual = bingEngine.ParseReponse(StringResources.BingTestJson).Result;

            var act = actual as BingResponse;
            expected.Should()
                .BeEquivalentTo(act, options => options.Including(o => o.webPages).Excluding(o => o.Name));
        }
    }
}
