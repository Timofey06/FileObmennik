using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FileObmennik.Models
{
    public class File
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string FileType { get; set; }

    }
}
