namespace CoverotNimorin.GollumChat.Server.Entities;

public class Note : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? LastModifiedAt { get; set; }

    public List<Picture> Pictures { get; set; } = new();

    public string UserId { get; set; } = string.Empty;
    public User? User { get; set; }
}