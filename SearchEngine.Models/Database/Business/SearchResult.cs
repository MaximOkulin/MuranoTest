using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SearchEngine.Models.Database.Business
{
    [Table("SearchResult", Schema = "Business")]
    public class SearchResult
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int SearchId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Url { get; set; }

        [ForeignKey("SearchId")]
        public Search Search { get; set; }
    }
}
