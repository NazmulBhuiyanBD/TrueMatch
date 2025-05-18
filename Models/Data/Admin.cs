using System.ComponentModel.DataAnnotations;

namespace TrueMatch.Models.Data
{
    public class Admin
    {
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
