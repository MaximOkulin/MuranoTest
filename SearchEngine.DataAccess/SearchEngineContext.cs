using Microsoft.EntityFrameworkCore;
using SearchEngine.Models.Database.Business;
using SearchEngine.Models.Database.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchEngine.Models
{
    public class SearchEngineContext : DbContext
    {
        public SearchEngineContext(DbContextOptions<SearchEngineContext> options) : base(options)
        {

        }

        public DbSet<SearchEngineType> SearchEngineTypes { get; set; }
        public DbSet<Search> Searchs { get; set; }
        public DbSet<SearchResult> SearchResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SearchEngineType>().HasData(new SearchEngineType { Id = 1, Code = "Google", Description = "Поисковая система Google" });
            modelBuilder.Entity<SearchEngineType>().HasData(new SearchEngineType { Id = 2, Code = "Yandex", Description = "Поисковая система Яндекс" });
            modelBuilder.Entity<SearchEngineType>().HasData(new SearchEngineType { Id = 3, Code = "Bing", Description = "Поисковая система Bing" });
        }

        public void SaveSearchResults(List<SearchResult> searchResults, string keyWords, string engineName)
        {
            var searchEngineType = SearchEngineTypes.FirstOrDefault(p => p.Code.Equals(engineName));
            if (searchEngineType != null)
            {
                var search = new Search
                {
                    KeyWords = keyWords,
                    Time = DateTime.Now,
                    SearchEngineTypeId = searchEngineType.Id
                };

                foreach (var searchResult in searchResults)
                {
                    searchResult.Search = search;
                }

                Searchs.Add(search);
                SearchResults.AddRange(searchResults);

                SaveChanges();
            }
        }
    }
}
