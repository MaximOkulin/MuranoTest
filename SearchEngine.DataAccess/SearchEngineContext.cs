using Microsoft.EntityFrameworkCore;
using SearchEngine.Models.Database.Business;
using SearchEngine.Models.Database.Dictionaries;

namespace SearchEngine.DataAccess
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
    }
}
