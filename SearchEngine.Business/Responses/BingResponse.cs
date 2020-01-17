using SearchEngine.Models.Database.Business;
using System.Collections.Generic;

namespace SearchEngine.Business.Responses
{
    /// <summary>
    /// Presents response from Bing
    /// </summary>
    public class BingResponse : Response
    {
        public Webpages webPages { get; set; }

        public override List<SearchResult> ToSearchResults()
        {
            if (webPages == null)
            {
                return null;
            }

            var searchResults = new List<SearchResult>();
            
            foreach(var v in webPages.value)
            {
                searchResults.Add(new SearchResult
                {
                    Description = v.name,
                    Url = v.url
                });
            }

            return searchResults;
        }
    }

    public class Webpages
    {
        public Value[] value { get; set; }
    }

    public class Value
    {
        public string name { get; set; }
        public string url { get; set; }
    }
}
