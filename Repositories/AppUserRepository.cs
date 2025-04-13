using Microsoft.EntityFrameworkCore;
using SeeFrontendTry002.Data;
using SeeFrontendTry002.Interface;

namespace SeeFrontendTry002.Repositories;

public class AppUserRepository: IAppUserRepository
{
    private readonly ApplicationDbContext _context;

    public AppUserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    

    public async Task<string?> GetUserIdByEmailAsync(string email)
    {
        var user = await _context.AppUsers
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email != null && u.Email.ToLower() == email.ToLower());

        return user?.Id;
    }

    // Implement SaveAsync (optional)
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}