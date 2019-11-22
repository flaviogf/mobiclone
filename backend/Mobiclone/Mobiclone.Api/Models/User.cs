using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mobiclone.Api.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [StringLength(255)]
        [Required]
        public string Email { get; set; }

        [StringLength(255)]
        [Required]
        public string PasswordHash { get; set; }

        [ForeignKey("File")]
        public int? FileId { get; set; }

        public File File { get; set; }
    }
}
