using SearchEngine.Models;
using SearchEngine.Models.Database.Business;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchEngine.Business.Interfaces
{
    public interface ISearcher
    {
        Task<SearchResultInfo> SearchAsync(string query);
    }
}
