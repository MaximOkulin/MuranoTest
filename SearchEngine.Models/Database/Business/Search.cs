using SearchEngine.Models.Database.Dictionaries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SearchEngine.Models.Database.Business
{
    [Table("Search", Schema ="Business")]
    public class Search
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public string KeyWords { get; set; }
        [Required]
        public int SearchEngineTypeId { get; set; }

        [ForeignKey("SearchEngineTypeId")]
        public SearchEngineType SearchEngineType { get; set; }

        [InverseProperty("Search")]
        public List<SearchResult> SearchResults { get; set; }
    }
}
