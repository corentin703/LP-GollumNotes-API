using CoverotNimorin.GollumNotes.Server.Entities;

namespace CoverotNimorin.GollumNotes.Server.Contracts.Services;

public interface ICurrentUserService
{
    public User? CurrentUser { get; }

    public User GetRequiredUser();
}