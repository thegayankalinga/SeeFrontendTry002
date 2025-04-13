using SeeFrontendTry002.Models;

namespace SeeFrontendTry002.Interface;

public interface IUserProfileRepository
{
    Task InitializeAsync();
    string? UserType { get; }
    AppUser? CurrentUser { get; }
}