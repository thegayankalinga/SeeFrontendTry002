using Microsoft.AspNetCore.Identity;
using SeeFrontendTry002.Data;
using SeeFrontendTry002.Interface;
using SeeFrontendTry002.Models;
using SeeFrontendTry002.Models.Enumz;

namespace SeeFrontendTry002.Repositories;

public class UserProfileRepository: IUserProfileRepository
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public string? UserType { get; private set; }
    public AppUser? CurrentUser { get; private set; }

    public UserProfileRepository(
        ApplicationDbContext context, 
        UserManager<AppUser> userManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }
    
    //This method to get the current Logged in user
    public async Task InitializeAsync()
    {
        if (_httpContextAccessor.HttpContext == null || 
            _httpContextAccessor.HttpContext.User.Identity is not { IsAuthenticated: true })
            return;
        
        CurrentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        if (CurrentUser == null) return;
            
        // Determine user type from roles
        if (_httpContextAccessor.HttpContext.User.IsInRole(UserRoles.Administrator))
        {
            UserType = UserRoles.Administrator;
                
        }
        else if (_httpContextAccessor.HttpContext.User.IsInRole(UserRoles.ProjectManager))
        {
            UserType = UserRoles.PreSalesEngineer;
        }
        else if (_httpContextAccessor.HttpContext.User.IsInRole(UserRoles.PreSalesEngineer))
        {
            UserType = UserRoles.PreSalesEngineer;
                
        }
    }
}