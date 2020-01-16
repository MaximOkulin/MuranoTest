using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SearchEngine.Models.Database.Dictionaries
{
    [Table("SearchEngineType", Schema ="Dictionaries")]
    public class SearchEngineType
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
