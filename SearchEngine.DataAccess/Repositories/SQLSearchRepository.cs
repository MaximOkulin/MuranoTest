using Microsoft.EntityFrameworkCore;
using SearchEngine.DataAccess.Interfaces;
using SearchEngine.Models.Database.Business;
using SearchEngine.Models.Database.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngine.DataAccess.Repositories
{
    public class SQLSearchRepository : IRepository
    {
        private SearchEngineContext _context;

        public SQLSearchRepository(SearchEngineContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns last search by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Search> GetLastSearchAsync()
        {
            return await _context.Set<Search>()
                .Include(s => s.SearchResults)
                .Include(s => s.SearchEngineType)
                .OrderByDescending(s => s.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<Search> DoSearchByLastAsync(string query)
        {
            var search = await _context.Set<Search>()
                .OrderByDescending(s => s.Id)
                .FirstAsync();

            await _context.Set<SearchResult>()
                .Where(sr => sr.SearchId == search.Id)
                .LoadAsync();

            await _context.Set<SearchEngineType>().LoadAsync();

            search.SearchResults = _context
                .Set<SearchResult>()
                .Local
                .Where(sr => sr.Description.Contains(query))
                .ToList();

            return search;
        }

        public async Task<int> SaveSearchResultsAsync(List<SearchResult> searchResults, string query, string engineName)
        {
            var searchEngineType = _context.SearchEngineTypes.FirstOrDefault(p => p.Code.Equals(engineName));
            if (searchEngineType != null)
            {
                var search = new Search
                {
                    KeyWords = query,
                    Time = DateTime.Now,
                    SearchEngineTypeId = searchEngineType.Id
                };

                foreach (var searchResult in searchResults)
                {
                    searchResult.Search = search;
                }

                _context.Searchs.Add(search);
                _context.SearchResults.AddRange(searchResults);

                return await _context.SaveChangesAsync();
            }

            return -1;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
