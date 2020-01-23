using SearchEngine.Business.Engines;
using Xunit;
using SearchEngine.Business.Interfaces;
using SearchEngine.Business.Responses;
using PublicResXFileCodeGenerator;
using Microsoft.Extensions.Options;
using FluentAssertions;
using SearchEngine.Business.Settings;
using RichardSzalay.MockHttp;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

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

            public void SetupHttpClientWithMock(MockHttpMessageHandler mockHttp)
            {
                HttpClient = new HttpClient(mockHttp);
            }
        }

        [Fact]
        public async Task<int> ExecuteAsync_GetGoodResponse()
        {
            // arrange
            IOptions<BingSettings> options = Options.Create<BingSettings>(new BingSettings
            {
                Name = StringResources.Bing,
                RequestFormat = "https://api.cognitive.microsoft.com/bing/v7.0/search?q={0}",
                Value = "Ocp-Apim-Subscription-Key"
            });

            var bingEngine = new BingEngineChild(options);
            string query = TestStringResources.BingDevExpressMVC5TreeViewQuery;

            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(TestStringResources.BingDevExpressMVC5TreeViewTestRequest)
                .Respond(StringResources.MIME_Json, TestStringResources.BingDevExpressMVC5TreeViewTestJson);

            bingEngine.SetupHttpClientWithMock(mockHttp);

            BingResponse expected = new BingResponse()
            {
                webPages = new Webpages()
                {
                    value = new Value[] { new Value { name = "AJAX - ASP.NET MVC Tree View Extension", url = "https://demos.devexpress.com/MVCxNavigationAndLayoutDemos/TreeView/Callbacks" },
                                          new Value { name = "ASP.NET MVC TreeView-Grid Hybrid Control", url = "https://demos.devexpress.com/MVCxTreeListDemos" },
                                          new Value { name = "Hierarchical Data Structure - DevExtreme Tree", url = "https://js.devexpress.com/.../TreeView/HierarchicalDataStructure/Mvc/Light" }
                    }
                }
            };

            // act
            var response = await bingEngine.ExecuteAsync(query, CancellationToken.None);

            // assert
            response.Should().BeEquivalentTo(expected, options => options.Including(o => o.webPages).Excluding(o => o.Name));

            return 0;
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
            var actual = bingEngine.ParseResponse(TestStringResources.BingTestJson);

            var act = actual as BingResponse;

            // assert
            act.Should()
                .BeEquivalentTo(expected, options => options.Including(o => o.webPages).Excluding(o => o.Name));
        }
    }
}
