using SeeFrontendTry002.Dtos;
using SeeFrontendTry002.Models.Enumz;

namespace SeeFrontendTry002.Mappers;

public static class PredictionApiRequestMapper
{
    private static string BoolToYesNo(bool? value) => value == true ? "yes" : "no";
    
    public static PredictionApiRequestDto ResponseDetailsToApiRequest(PredictionResponseDetailsDto inputFeatures, PredictionModel predictionModel)
    {
        var features = new List<string>
        {
            //Region (should bea one of value form the Region Enum
            inputFeatures.Region.GetTypeCode().ToString(),
            
            //Environments
            BoolToYesNo(inputFeatures.FeatureData.DevEnvironment),
            BoolToYesNo(inputFeatures.FeatureData.SitEnvironment),
            BoolToYesNo(inputFeatures.FeatureData.UatEnvironment),
            BoolToYesNo(inputFeatures.FeatureData.StagingEnvironment),
            BoolToYesNo(inputFeatures.FeatureData.DevEnvironment),
            BoolToYesNo(inputFeatures.FeatureData.DevEnvironment),
            BoolToYesNo(inputFeatures.FeatureData.DevEnvironment),
            
            //Compliance
            BoolToYesNo(inputFeatures.FeatureData.CompliancePciSff),
            BoolToYesNo(inputFeatures.FeatureData.CountrySpecificCompliance),
            
            //Technology
            inputFeatures.FeatureData.BackendType.GetTypeCode().ToString(),
            inputFeatures.FeatureData.FrontendType.GetTypeCode().ToString(),
            inputFeatures.FeatureData.MobileType.GetTypeCode().ToString(),
            inputFeatures.FeatureData.DatabaseType.GetTypeCode().ToString(),
            
            //SSO
            BoolToYesNo(inputFeatures.FeatureData.GoogleSso),
            BoolToYesNo(inputFeatures.FeatureData.AppleSso),
            BoolToYesNo(inputFeatures.FeatureData.FacebookSso),
            inputFeatures.FeatureData.IamVendor.GetTypeCode().ToString(),
            
            //Infra
            inputFeatures.FeatureData.InfraProvider.GetTypeCode().ToString(),
            
            //Qualitative Features
            inputFeatures.FeatureData.DependencyComplexity.GetTypeCode().ToString(),
            inputFeatures.FeatureData.DecisionSpeed.GetTypeCode().ToString(),
            inputFeatures.FeatureData.ClientTechnicalKnowledge.GetTypeCode().ToString(),
            inputFeatures.FeatureData.DeviceTestCoverage.GetTypeCode().ToString(),
            BoolToYesNo(inputFeatures.FeatureData.TestAutomation),
            inputFeatures.FeatureData.Regression.GetTypeCode().ToString(),
            
            //Other Services
            BoolToYesNo(inputFeatures.FeatureData.MiddlewareAvailability),
            BoolToYesNo(inputFeatures.FeatureData.PaymentProviderIntegration),
            BoolToYesNo(inputFeatures.FeatureData.FidoIntegration),
            BoolToYesNo(inputFeatures.FeatureData.DataMigration),
            
            //Language
            inputFeatures.FeatureData.NoOfLanguages.ToString(),
            inputFeatures.FeatureData.NoOfRtlLanguages.ToString(),
            
            //Quantitative
            inputFeatures.FeatureData.TpsRequired.ToString(),
            inputFeatures.FeatureData.WarrantyMonths.ToString(),
            inputFeatures.FeatureData.NoOfFunctionalModules.ToString(),
            inputFeatures.FeatureData.NoOfNoneFunctionalModules.ToString(),
            inputFeatures.FeatureData.NoOfUatCycles.ToString(),
            inputFeatures.FeatureData.CodeTestCoverage.ToString("0.##"),
            inputFeatures.FeatureData.RestIntegrationPoints.ToString(),
            inputFeatures.FeatureData.SoapIntegrationPoints.ToString(),
            inputFeatures.FeatureData.Iso8583IntegrationPoints.ToString(),
            inputFeatures.FeatureData.SdkIntegrationPoints.ToString(),
            
        };
        
        var featureNames = new List<string>
        {
            "region", "dev_environment", "sit_environment", "uat_environment",
            "staging_environment", "training_environment", "production_environment", "dr_environment",
            "compliance_pci_sff", "compliance_country_specific",
            "backend_technology", "frontend_technology", "mobile_technology", "database",
            "google_sso", "apple_sso", "facebook_sso", "iam_vendor", "infrastructure_type",
            "dependency_complexity", "customer_decision_speed", "client_technical_knowledge", "device_test_coverage",
            "test_automation", "regression_type", "middleware_availability", "payment_provider_integration",
            "fido", "data_migration", "no_of_languages", "no_of_rtl_languages",
            "tps_required", "warranty_months", "no_of_functional_modules", "no_of_none_functional_modules",
            "uat_cycles", "test_coverage", "rest_integration_points", "soap_integration_points",
            "iso8583_integration_points", "sdk_integration_points"
        };
        
        // Log the features
        for (int i = 0; i < featureNames.Count; i++)
        {
            Console.WriteLine($"{featureNames[i],-35} => {features[i]}");
        }
        
        return new PredictionApiRequestDto()
        {
            Features = features.Select(f => f.ToString()).ToList(),
            ModelName = predictionModel.GetTypeCode().ToString(),
        };
    }
    
    
    
}