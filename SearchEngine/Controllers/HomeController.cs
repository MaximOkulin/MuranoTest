using System;
using System.Linq;
using System.Threading.Tasks;
//using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SearchEngine.Business.Interfaces;
using SearchEngine.Models;
using SearchEngine.Models.Database.Business;
using SearchEngine.Models.Database.Dictionaries;

namespace SearchEngine.Controllers
{
    public class HomeController : Controller
    {
        private readonly SearchEngineContext _context;
        private ISearcher _searcher;

        public HomeController
        (
            ISearcher searcher, 
            SearchEngineContext context
        )
        {
            _searcher = searcher;
            _context = context;
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
                _context.SaveSearchResults(searchResultInfo.SearchResults, searchTemplate.Template, searchResultInfo.EngineName);
            }

            ViewBag.SearcherName = searchResultInfo.EngineName;
            ViewBag.Time = DateTime.Now;

            return PartialView("SearchResultTable", searchResultInfo.SearchResults);
        }

        public async Task<IActionResult> LocalSearch([FromBody]SearchTemplate searchTemplate)
        {
            searchTemplate.Template = searchTemplate.Template ?? string.Empty;

            var search =  await _context.Set<Search>()
                .OrderByDescending(s => s.Id)
                .FirstAsync();

            await _context.Set<SearchResult>()
                .Where(sr => sr.SearchId == search.Id)
                .LoadAsync();

            await _context.Set<SearchEngineType>().LoadAsync();

            search.SearchResults = _context
                .Set<SearchResult>()
                .Local
                .Where(sr => sr.Description.Contains(searchTemplate.Template))
                .ToList();

            if (search != null)
            {
                ViewBag.SearcherName = search.SearchEngineType.Code;
                ViewBag.Time = search.Time;
            }
            

            return PartialView("SearchResultTable", search.SearchResults);
        }

        public async Task<IActionResult> LocalSearchPage()
        {
            var search =  await _context.Set<Search>()
                .Include(s => s.SearchResults)
                .Include(s => s.SearchEngineType)
                .OrderByDescending(s => s.Id)
                .FirstAsync();

            if (search != null)
            {
                ViewBag.SearcherName = search.SearchEngineType.Code;
                ViewBag.Time = search.Time;
            }

            return View("LocalSearchPage", search);
        }
    }
}
