using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class Memo
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string? Title { get; set; }
        
        public DateTime CreationDate { get; set; }
        
        public string? AudioFilePath { get; set; }
    }

}
