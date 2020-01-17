using SearchEngine.Models.Database.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchEngine.DataAccess.Interfaces
{
    public interface IRepository : IDisposable
    {
        /// <summary>
        /// Returns last Search object by Id
        /// </summary>
        /// <returns></returns>
        Task<Search> GetLastSearchAsync();

        /// <summary>
        /// Do search in last search tuple
        /// </summary>
        /// <returns></returns>
        Task<Search> DoSearchByLastAsync(string query);

        /// <summary>
        /// Saves search results
        /// </summary>
        /// <param name="searchResults"></param>
        /// <param name="query"></param>
        /// <param name="engineName"></param>
        void SaveSearchResultsAsync(List<SearchResult> searchResults, string query, string engineName);
    }
}
