using System.ComponentModel.DataAnnotations;

namespace TrueMatch.Areas.Admin
{
    public class AdminAccount
    {
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
