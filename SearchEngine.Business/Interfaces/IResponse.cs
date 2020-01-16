using SearchEngine.Models.Database.Business;
using System.Collections.Generic;

namespace SearchEngine.Business.Interfaces
{
    public interface IResponse
    {
        List<SearchResult> ToSearchResults();
        string Name { get; }
    }
}
