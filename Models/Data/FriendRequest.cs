using System.ComponentModel.DataAnnotations;

public class FriendRequest
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string SenderEmail { get; set; }

    [Required]
    public string ReceiverEmail { get; set; }

    public DateTime RequestedAt { get; set; } = DateTime.Now;

    public bool IsAccepted { get; set; } = false;
}
