using Microsoft.Extensions.Options;
using PublicResXFileCodeGenerator;
using SearchEngine.Business.Engines;
using SearchEngine.Business.Interfaces;
using SearchEngine.Business.Responses;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using SearchEngine.Business.Settings;

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
