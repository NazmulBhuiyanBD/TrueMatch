using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrueMatch.Models.Data
{
    public class Friend
    {
        [Key]
        public int Id { get; set; }

        public string RequesterEmail { get; set; }  // Who sent the request
        public string ReceiverEmail { get; set; }   // Who received the request

        public DateTime RequestDate { get; set; }

        public bool IsAccepted { get; set; }  // true = accepted, false = pending
    }
}
