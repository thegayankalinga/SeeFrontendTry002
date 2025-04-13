using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using SeeFrontendTry002.Data;
using SeeFrontendTry002.Models.Enumz;

namespace SeeFrontendTry002.Models;

public class AppUser: IdentityUser
{
    [MaxLength(100)]
    public required string FirstName { get; set; }
    [MaxLength(100)]
    public string? LastName { get; set; }
    public UserRoleType Role { get; set; }

    public ICollection<Project> Projects { get; set; } = new List<Project>();
}