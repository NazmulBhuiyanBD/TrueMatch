using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrueMatch.Models.Data
{
    public class UserInfo
    {
        [Key]
        public Guid UserID { get; set; }

        [Required]
        [ForeignKey(nameof(Account))] // Refers to the navigation property below
        public string Email { get; set; }

        public Account Account { get; set; } // Navigation property

        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        [StringLength(10)]
        public string? Gender { get; set; }

        [MaxLength(200)]
        public string? AboutUser { get; set; }

        public string? ImageUrl { get; set; }
    }
}
