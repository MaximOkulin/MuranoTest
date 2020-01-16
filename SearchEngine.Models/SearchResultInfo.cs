using SearchEngine.Models.Database.Business;
using System.Collections.Generic;

namespace SearchEngine.Models
{
    public class SearchResultInfo
    {
        public List<SearchResult> SearchResults { get; set; }
        public string EngineName { get; set; }
    }
}
