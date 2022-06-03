namespace CoverotNimorin.GollumNotes.Server.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public List<Note> Notes { get; set; } = new();
}