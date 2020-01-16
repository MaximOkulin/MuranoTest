using PublicResXFileCodeGenerator;
using SearchEngine.Business.Interfaces;
using SearchEngine.Models.Database.Business;
using System.Collections.Generic;

namespace SearchEngine.Business.Responses
{
    /// <summary>
    /// Base class for responses
    /// </summary>
    public abstract class Response : IResponse
    {
        public string Name
        {
            get => GetType().Name.Replace(StringResources.Response, string.Empty);            
        }

        public abstract List<SearchResult> ToSearchResults();
    }
}
