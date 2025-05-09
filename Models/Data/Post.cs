using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrueMatch.Models.Data
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Account")]
        public string Email { get; set; }
        public Account? Account { get; set; }
        public string ? PostTitle { get; set; }
        public string Description { get; set; }

        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
