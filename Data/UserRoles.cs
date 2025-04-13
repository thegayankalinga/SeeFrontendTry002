using SeeFrontendTry002.Models.Enumz;

namespace SeeFrontendTry002.Data;

public static class UserRoles
{
    public const string ProjectManager = "pm";
    public const string Administrator = "admin";
    public const string PreSalesEngineer = "presales";

    public static string ToRoleString(UserRoleType role)
    {
        return role switch
        {
            UserRoleType.Administrator => Administrator,
            UserRoleType.ProjectManager => ProjectManager,
            UserRoleType.PreSalesEngineer => PreSalesEngineer,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public static UserRoleType FromString(string role)
    {
        return role.ToLower() switch
        {
            Administrator => UserRoleType.Administrator,
            ProjectManager => UserRoleType.ProjectManager,
            PreSalesEngineer => UserRoleType.PreSalesEngineer,
            _ => throw new ArgumentException("Invalid role string", nameof(role))
        };
    }
}