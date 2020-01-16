using SearchEngine.Business.Engines;
using Xunit;
using SearchEngine.Business.Interfaces;
using SearchEngine.Business.Responses;
using PublicResXFileCodeGenerator;
using Microsoft.Extensions.Options;
using FluentAssertions;
using SearchEngine.Business.Settings;

namespace SearchEngine.Tests
{
    
    public class BingEngineTests
    {
        private class BingEngineChild : BingEngine
        {
            public BingEngineChild(IOptions<BingSettings> settings) : base(settings)
            {
                
            }

            public new IResponse ParseResponse(string response)
            {
                return base.ParseResponse(response);
            }
        }

        [Fact]
        public void ParseResponse_Should_BeEquivalentTo_With_3Items()
        {
            // arrange
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

            // act
            var actual = bingEngine.ParseResponse(StringResources.BingTestJson);

            var act = actual as BingResponse;

            // assert
            expected.Should()
                .BeEquivalentTo(act, options => options.Including(o => o.webPages).Excluding(o => o.Name));
        }
    }
}
