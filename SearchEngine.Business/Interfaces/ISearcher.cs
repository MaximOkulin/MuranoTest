using SearchEngine.Models;
using System.Threading.Tasks;

namespace SearchEngine.Business.Interfaces
{
    public interface ISearcher
    {
        Task<SearchResultInfo> SearchAsync(string query);
    }
}
