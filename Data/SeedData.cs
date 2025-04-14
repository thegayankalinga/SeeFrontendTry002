using Microsoft.AspNetCore.Identity;
using SeeFrontendTry002.Models;
using SeeFrontendTry002.Models.Enumz;

namespace SeeFrontendTry002.Data;

public abstract class SeedData
{
    public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Roles
        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        if (!await roleManager.RoleExistsAsync(UserRoles.Administrator))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Administrator));
        if (!await roleManager.RoleExistsAsync(UserRoles.ProjectManager))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.ProjectManager));
        if (!await roleManager.RoleExistsAsync(UserRoles.PreSalesEngineer))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.PreSalesEngineer));

        // Users
        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

        // Admin
        string adminUserEmail = "admin1@see.com";
        var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
        if (adminUser == null)
        {
            var newAdminUser = new AppUser
            {
                UserName = adminUserEmail,
                Email = adminUserEmail,
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "001",
            };
            await userManager.CreateAsync(newAdminUser, "Test@123");
            await userManager.AddToRoleAsync(newAdminUser, UserRoles.Administrator);
        }

        // Couple
        string pmEmail = "pm1@see.com";
        var pmUser = await userManager.FindByEmailAsync(pmEmail);
        if (pmUser == null)
        {
            pmUser = new AppUser
            {
                UserName = pmEmail,
                Email = pmEmail,
                EmailConfirmed = true,
                FirstName = "Project",
                LastName = "Manager 001"
            };
            await userManager.CreateAsync(pmUser, "Test@123");
            await userManager.AddToRoleAsync(pmUser, UserRoles.ProjectManager);
        }
            

        // Planners
        string[] pseEmails =
        [
            "pse001@see.com",
            "pse002@see.com",
            "pse003@see.com",
            "pse004@see.com"
        ];

        for (int i = 0; i < pseEmails.Length; i++)
        {
            var pseUser = await userManager.FindByEmailAsync(pseEmails[i]);
            if (pseUser == null)
            {
                pseUser = new AppUser
                {
                    UserName = pseEmails[i],
                    Email = pseEmails[i],
                    EmailConfirmed = true,
                    FirstName = $"Pre Sales",
                    LastName = $"Engineer {i + 1}"
                };
                await userManager.CreateAsync(pseUser, "Test@123");
                await userManager.AddToRoleAsync(pseUser, UserRoles.PreSalesEngineer);
            }
                
        }

        await context.SaveChangesAsync();
    }
    
    public static async Task SeedSampleProjectsAndMetaAsync(IApplicationBuilder applicationBuilder)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (context.Projects.Any())
            return;

        var projects = new List<Project>
        {
            new Project
            {
                ProjectName = "Project_1",
                Region = Region.Gcc,
                FeatureData = new FeatureData
                {
                    DevEnvironment = true,
                    SitEnvironment = true,
                    UatEnvironment = true,
                    StagingEnvironment = true,
                    TrainingEnvironment = true,
                    ProductionEnvironment = true,
                    DrEnvironment = false,
                    
                    CompliancePciSff = true,
                    CountrySpecificCompliance = true,
                    
                    BackendType = BackendType.Java,
                    FrontendType = FrontendType.ReactJs,
                    MobileType = MobileType.Flutter,
                    DatabaseType = DatabaseType.PostgresSql,
                    
                    GoogleSso = true,
                    AppleSso = false,
                    FacebookSso = true,
                    IamVendor = IamVendorType.Keycloak,
                    
                    InfraProvider = InfraType.Azure,
                    
                    DependencyComplexity = RatingType.High,
                    DecisionSpeed = SpeedType.Fast,
                    ClientTechnicalKnowledge = RatingType.Medium,
                    DeviceTestCoverage = RatingType.High,
                    TestAutomation = true,
                    Regression = RegressionType.PartialRegression,
                    MiddlewareAvailability = true,
                    PaymentProviderIntegration = true,
                    FidoIntegration = true,
                    DataMigration = false,
                    
                    NoOfLanguages = 3,
                    NoOfRtlLanguages = 1,
                    TpsRequired = 600,
                    WarrantyMonths = 3,
                    NoOfFunctionalModules = 200,
                    NoOfNoneFunctionalModules = 28,
                    
                    NoOfUatCycles = 2,
                    CodeTestCoverage = (float)0.75,
                    RestIntegrationPoints = 20,
                    SoapIntegrationPoints = 9,
                    Iso8583IntegrationPoints = 0,
                    SdkIntegrationPoints = 1,
                }
            },
            new Project
            {
                ProjectName = "Project_2",
                Region = Region.Gcc,
                FeatureData = new FeatureData
                {
                    DevEnvironment = true,
                    SitEnvironment = true,
                    UatEnvironment = true,
                    StagingEnvironment = true,
                    TrainingEnvironment = false,
                    ProductionEnvironment = true,
                    DrEnvironment = false,

                    CompliancePciSff = true,
                    CountrySpecificCompliance = true,

                    BackendType = BackendType.Java,
                    FrontendType = FrontendType.ReactJs,
                    MobileType = MobileType.Flutter,
                    DatabaseType = DatabaseType.PostgresSql,

                    GoogleSso = false,
                    AppleSso = false,
                    FacebookSso = false,
                    IamVendor = IamVendorType.Keycloak,

                    InfraProvider = InfraType.Azure,

                    DependencyComplexity = RatingType.High,
                    DecisionSpeed = SpeedType.Fast,
                    ClientTechnicalKnowledge = RatingType.Medium,
                    DeviceTestCoverage = RatingType.High,
                    TestAutomation = true,
                    Regression = RegressionType.PartialRegression,
                    MiddlewareAvailability = true,
                    PaymentProviderIntegration = true,
                    FidoIntegration = true,
                    DataMigration = false,

                    NoOfLanguages = 1,
                    NoOfRtlLanguages = 3,
                    TpsRequired = 800,
                    WarrantyMonths = 6,
                    NoOfFunctionalModules = 157,
                    NoOfNoneFunctionalModules = 85,

                    NoOfUatCycles = 3,
                    CodeTestCoverage = 0.7f,
                    RestIntegrationPoints = 26,
                    SoapIntegrationPoints = 1,
                    Iso8583IntegrationPoints = 0,
                    SdkIntegrationPoints = 1,
                }
            },

            new Project
            {
                ProjectName = "Project_3",
                Region = Region.Gcc,
                FeatureData = new FeatureData
                {
                    DevEnvironment = true,
                    SitEnvironment = false,
                    UatEnvironment = true,
                    StagingEnvironment = false,
                    TrainingEnvironment = false,
                    ProductionEnvironment = true,
                    DrEnvironment = false,

                    CompliancePciSff = true,
                    CountrySpecificCompliance = true,

                    BackendType = BackendType.Java,
                    FrontendType = FrontendType.ReactJs,
                    MobileType = MobileType.Flutter,
                    DatabaseType = DatabaseType.PostgresSql,

                    GoogleSso = false,
                    AppleSso = false,
                    FacebookSso = false,
                    IamVendor = IamVendorType.Keycloak,

                    InfraProvider = InfraType.Azure,

                    DependencyComplexity = RatingType.High,
                    DecisionSpeed = SpeedType.Fast,
                    ClientTechnicalKnowledge = RatingType.Medium,
                    DeviceTestCoverage = RatingType.High,
                    TestAutomation = true,
                    Regression = RegressionType.PartialRegression,
                    MiddlewareAvailability = true,
                    PaymentProviderIntegration = true,
                    FidoIntegration = true,
                    DataMigration = false,

                    NoOfLanguages = 2,
                    NoOfRtlLanguages = 1,
                    TpsRequired = 1000,
                    WarrantyMonths = 6,
                    NoOfFunctionalModules = 176,
                    NoOfNoneFunctionalModules = 70,

                    NoOfUatCycles = 3,
                    CodeTestCoverage = 0.6f,
                    RestIntegrationPoints = 23,
                    SoapIntegrationPoints = 3,
                    Iso8583IntegrationPoints = 1,
                    SdkIntegrationPoints = 1,
                }
            }
        };

        context.Projects.AddRange(projects);
        await context.SaveChangesAsync();
    }
}