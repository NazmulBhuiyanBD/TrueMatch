using System.ComponentModel.DataAnnotations;

namespace TrueMatch.Models.Data
{
    public class Account
    {
        [Key]
        public required string Email { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
