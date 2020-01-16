using SearchEngine.Business.Interfaces;
using SearchEngine.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SearchEngine.Business.Services
{
    public class Searcher : ISearcher
    {
        private List<IEngine> _searchEngines;

        public Searcher(IEnumerable<IEngine> searchEngines)
        {
            _searchEngines = searchEngines.ToList();
        }

        /// <summary>
        /// Executes asynchronous search
        /// </summary>
        /// <param name="keyWords"></param>
        /// <returns></returns>

        public async Task<SearchResultInfo> SearchAsync(string query)
        {
            if (query == null)
            {
                return null;
            }

            List<Task<IResponse>> tasks = new List<Task<IResponse>>();

            // max. 10 seconds for response
            var cts = new CancellationTokenSource(10000);

            foreach (var searchEngine in _searchEngines)
            {
                tasks.Add(searchEngine.Execute(query, cts.Token));
            }

            SearchResultInfo searchResultInfo = new SearchResultInfo();

            while (tasks.Count > 0)
            {
                Task<IResponse> currentFinishedTask = await Task.WhenAny(tasks);

                if (currentFinishedTask.Status == TaskStatus.RanToCompletion)
                {
                    var response = currentFinishedTask.Result;
                    var searchResult = response.ToSearchResults();
                    if (searchResult.Count > 0)
                    {
                        searchResultInfo.SearchResults = searchResult;
                        searchResultInfo.EngineName = response.Name;
                        cts.Cancel();
                        break;
                    }
                }
                else
                {
                    tasks.Remove(currentFinishedTask);
                }
            }

            return searchResultInfo;
        }
    }
}
