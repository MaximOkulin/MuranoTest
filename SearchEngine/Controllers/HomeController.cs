using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SearchEngine.Business.Interfaces;
using SearchEngine.DataAccess.Interfaces;
using SearchEngine.Models;

namespace SearchEngine.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;
        private ISearcher _searcher;

        public HomeController
        (
            ISearcher searcher,
            IRepository repository
        )
        {
            _searcher = searcher;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public class SearchTemplate
        {
            public string Template { get; set; }
        }
             
        public async Task<IActionResult> GlobalSearch([FromBody]SearchTemplate searchTemplate)
        {
            var searchResultInfo = await _searcher.SearchAsync(searchTemplate.Template);

            if (searchResultInfo == null)
            {
                searchResultInfo = new SearchResultInfo();
            }
            else
            {
                _repository.SaveSearchResultsAsync(searchResultInfo.SearchResults, searchTemplate.Template, searchResultInfo.EngineName);
            }

            ViewBag.SearcherName = searchResultInfo.EngineName;
            ViewBag.Time = DateTime.Now;

            return PartialView("SearchResultTable", searchResultInfo.SearchResults);
        }

        public async Task<IActionResult> LocalSearch([FromBody]SearchTemplate searchTemplate)
        {
            searchTemplate.Template = searchTemplate.Template ?? string.Empty;

            var search = await _repository.DoSearchByLastAsync(searchTemplate.Template);

            if (search != null)
            {
                ViewBag.SearcherName = search.SearchEngineType.Code;
                ViewBag.Time = search.Time;
            }
            

            return PartialView("SearchResultTable", search.SearchResults);
        }

        public async Task<IActionResult> LocalSearchPage()
        {
            var search = await _repository.GetLastSearchAsync();

            if (search != null)
            {
                ViewBag.SearcherName = search.SearchEngineType.Code;
                ViewBag.Time = search.Time;
            }

            return View("LocalSearchPage", search);
        }
    }
}
