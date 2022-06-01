namespace CoverotNimorin.GollumChat.Server.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    private List<Note> Notes { get; set; } = new();
}