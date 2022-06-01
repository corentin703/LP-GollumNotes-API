using CoverotNimorin.GollumChat.Server.Entities;

namespace CoverotNimorin.GollumChat.Server.Contracts.Services;

public interface ICurrentUserService
{
    public User? CurrentUser { get; }

    public User GetRequiredUser();
}