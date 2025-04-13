namespace SeeFrontendTry002.Interface;

public interface IAppUserRepository
{
    Task<string?> GetUserIdByEmailAsync(string email);
    
}