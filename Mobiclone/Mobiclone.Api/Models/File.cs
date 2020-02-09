using System.ComponentModel.DataAnnotations;

namespace Mobiclone.Api.Models
{
    public class File
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [StringLength(255)]
        [Required]
        public string Path { get; set; }
    }
}
