using System.ComponentModel.DataAnnotations;

namespace TrueMatch.Models.Data
{
    public class Account
    {
        [Key]
        public string Email { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        [StringLength(10)]
        public string? Gender { get; set; }

        public int? Age { get; set; }

        [StringLength(10)]
        public string? City { get; set; }

        [MaxLength(200)]
        public string? AboutUser { get; set; }
        public string? Address { get; set; }

        public string? ProfileImageUrl { get; set; }
        public string? BackGroundImageUrl { get; set; }

        public ICollection<FriendRequest> SentFriendRequests { get; set; }
        public ICollection<FriendRequest> ReceivedFriendRequests { get; set; }

    }
}
