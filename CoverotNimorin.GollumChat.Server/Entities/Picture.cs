namespace CoverotNimorin.GollumChat.Server.Entities;

public class Picture : BaseEntity
{
    public byte[] Content { get; set; } = Array.Empty<byte>();
    
    public string NoteId { get; set; } = string.Empty;
    public Note? Note { get; set; }
}