using System.ComponentModel.DataAnnotations;

namespace TrueMatch.Models.Data
{
    public class Complain
    {
        [Key]
        public Guid ComplainId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
