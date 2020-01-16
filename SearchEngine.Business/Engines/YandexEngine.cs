using Microsoft.Extensions.Options;
using SearchEngine.Business.Extensions;
using SearchEngine.Business.Interfaces;
using SearchEngine.Business.Responses;
using SearchEngine.Models.Settings;
using System.IO;
using System.Xml.Serialization;
using System;
using System.Text.RegularExpressions;

namespace SearchEngine.Business.Engines
{
    public class YandexEngine : BaseEngine
    {
        public YandexEngine(IOptions<YandexSettings> settings)
        {
            Settings = settings.Value;
        }

        protected override IResponse ParseResponse(string response)
        {
            if (response != null)
            {
                response = response.RemoveWrongSymbolsFromYandexResponse();

                yandexsearch pkg = null;

                try
                {
                    var serializer = new XmlSerializer(typeof(yandexsearch));
                    using (var stringReader = new StringReader(response))
                    {
                        pkg = (yandexsearch)serializer.Deserialize(stringReader);
                    }
                }
                catch
                {

                }

                if (pkg != null)
                {
                    var groups = pkg.response.results.grouping.group;

                    foreach(var gr in groups)
                    {
                        gr.doc.title = Regex.Replace(gr.doc.title, @"\n", string.Empty);
                    }

                    return pkg;
                }
            }

            return null;
        }

    }
}
