using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrueMatch.Models.Data
{
    public class Message
    {
        [Key]
        public Guid MsgId { get; set; }

        public string SenderEmail { get; set; }

        [ForeignKey("Email")]
        public string ReceiverEmail { get; set; }
        public Account Email { get; set; }

        public DateTime MsgTime { get; set; }

        public string MessageText { get; set; }
    }
}
