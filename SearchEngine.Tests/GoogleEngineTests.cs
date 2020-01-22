using Microsoft.Extensions.Options;
using PublicResXFileCodeGenerator;
using SearchEngine.Business.Engines;
using SearchEngine.Business.Interfaces;
using SearchEngine.Business.Responses;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using SearchEngine.Business.Settings;
using RichardSzalay.MockHttp;
using System.Net.Http;
using System.Threading;
using System;

namespace SearchEngine.Tests
{
    public class GoogleEngineTests
    {
        private class GoogleEngineChild : GoogleEngine
        {
            public GoogleEngineChild(IOptions<GoogleSettings> settings) : base(settings)
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
        public void Execute_GetGoodResponse()
        {
            // arrange
            IOptions<GoogleSettings> options = Options.Create<GoogleSettings>(new GoogleSettings
            {
                Name = "Google",
                RequestFormat = "https://www.googleapis.com/customsearch/v1?{0}&q={1}&cx=008892241395134864135:cu97aeslmrd&num=10",
                Value = "key=AIzaSyDjIxUEe6flD99KChSJ3248Lc_4E_FbtCo"
            });

            var googleEngine = new GoogleEngineChild(options);
            string query = "Empire State Building";

            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://www.googleapis.com/customsearch/v1?key=AIzaSyDjIxUEe6flD99KChSJ3248Lc_4E_FbtCo&q=Empire%2BState%2BBuilding&cx=008892241395134864135:cu97aeslmrd&num=10").
                Respond("application/json", StringResources.GoogleEmpireStateBuildingTestJson);

            googleEngine.SetupHttpClientWithMock(mockHttp);

            var cancellationTokenSource = new CancellationTokenSource();

            GoogleResponse expected = new GoogleResponse()
            {
                Items = new List<Item>
                {
                    new Item { Title = "Places to Visit in New York - Observation Deck | Empire State Building", Link = "https://u2556513ml.ha002.t.justns.ru/link.php?u=Urls://h4x5p4h4z2p2v2v4s4t4z374y5i5l4q484q2k5m4t5w2i5t4m4u5m4m4l564d4k4i5e584g5k4t4o484e4w574n224l4o4" },
                    new Item { Title = "Category:Empire State Building in fiction - Wikipedia", Link = "http://en.wiki.bks-tv.ru/wiki/Category:Empire_State_Building_in_fiction"},
                    new Item { Title = "Lawyer dies in Empire suicide horror - New York Daily News", Link = "https://amp.ng.ru/mobile/ng-ru/amp/?p=http://www.nydailynews.com/news/2007/04/14/2007-04-14_lawyer_dies_in_empire_suicide_horror.html"},
                }
            };

            // act
            var response = googleEngine.Execute(query, cancellationTokenSource.Token).Result;
            var act = new GoogleResponse();

            if (response is GoogleResponse resp)
            {
                act = resp;
            }
            else
            {
                throw new System.Exception("Recieved response is not GoogleResponse");
            }

            // assert
            expected.Should().BeEquivalentTo(act, options => options.Including(o => o.Items).Excluding(o => o.Name));
        }

        [Fact]
        public void ParseResponse_Should_BeEquivalentTo_With_9Items()
        {
            // arrange
            IOptions<GoogleSettings> options = Options.Create<GoogleSettings>(new GoogleSettings());
            var googleEngine = new GoogleEngineChild(options);

            GoogleResponse expected = new GoogleResponse()
            {
                Items = new List<Item>
                {
                    new Item { Title = "Hilton Hotel St. Petersburg Expoforum - St. Petersburg Hotels", Link = "http://link.2gis.ru/1.2/18C93A7F/online/20181101/project38/70000001027259359/2gis.ru/nl8cup42p4G2311302IGGGe0oynnis73G6G7734H4H569J8Hwpqt54923J121G3G732J3JG732puuv572B8B12848B1H28CH?http://SaintPetersburgExpoForum.Hilton.com" },
                    new Item { Title = "Saint Petersburg Lyceum 239 - Wikipedia", Link = "http://en.wiki.bks-tv.ru/wiki/Saint_Petersburg_Lyceum_239"},
                    new Item { Title = "Hotel in Saint Petersburg | Best Western Plus Centre Hotel", Link = "https://www.bestwestern.ru/en/hotels/best-western-plus-centre-hotel-saint-petersburg-91215"},
                    new Item { Title = "Saint Petersburg", Link = "https://tesera.ru/images/items/13552/Saint_Petersburg_Rules_EN.pdf"},
                    new Item { Title = "Saint Petersburg University: SPBU", Link = "https://english.spbu.ru/"},
                    new Item { Title = "Saint Petersburg, Russia", Link = "https://www.airpano.ru/files/Saint-Petersburg-Virtual-Tour/2-2"},
                    new Item { Title = "St.Petersburg Symphony Orchestra - Orchestras - St. Petersburg ...", Link = "https://www.philharmonia.spb.ru/en/about/orchestra/asof/"},
                    new Item { Title = "SKA (Saint Petersburg) | Info : Kontinental Hockey League (KHL)", Link = "https://en.khl.ru/clubs/ska/"},
                    new Item { Title = "Centre Hotel, Saint Petersburg, Russia - Booking.com", Link = "https://tonkosti.ru/book/hotel/ru/oktiabrskaya-ligovskiy-saint-petersburg.html?tab=4&aid=354965&label=RUS-HPREV1-HTLPAG-St._Petersburg" }
                }
            };

            // act
            var actual = googleEngine.ParseResponse(StringResources.GoogleTestJson);
            var act = actual as GoogleResponse;

            // assert
            expected.Should().BeEquivalentTo(act, options => options.Including(o => o.Items).Excluding(o => o.Name));
        }
    }
}
