using SeeFrontendTry002.Dtos;
using SeeFrontendTry002.Models;
using SeeFrontendTry002.Models.Enumz;
using SeeFrontendTry002.ViewModels;

namespace SeeFrontendTry002.Mappers;

public static class ProjectMapper
{
    //Request DTO to Project Model
    public static (
        Project project, 
        FeatureData featureData) ToEntities(PredictionRequestSaveDto dto)
    {
        

        var project = new Project
        {
            ProjectName = dto.ProjectName,
            Region = dto.Region,
            CalculationStatus = dto.CalculationStatus,
        };

        var feature = dto.FeatureData;

        var featureData = new FeatureData
        {
            
            
            // Environments
            DevEnvironment = feature.DevEnvironment,
            SitEnvironment = feature.SitEnvironment,
            UatEnvironment = feature.UatEnvironment,
            StagingEnvironment = feature.StagingEnvironment,
            TrainingEnvironment = feature.TrainingEnvironment,
            ProductionEnvironment = feature.ProductionEnvironment,
            DrEnvironment = feature.DrEnvironment,

            // Compliance
            CompliancePciSff = feature.CompliancePciSff,
            CountrySpecificCompliance = feature.CountrySpecificCompliance,

            // Technology
            BackendType = feature.BackendType,
            FrontendType = feature.FrontendType,
            MobileType = feature.MobileType,
            DatabaseType = feature.DatabaseType,

            // SSO
            GoogleSso = feature.GoogleSso,
            AppleSso = feature.AppleSso,
            FacebookSso = feature.FacebookSso,
            IamVendor = feature.IamVendor,

            // Infra
            InfraProvider = feature.InfraProvider,

            // Ratings & Attributes
            DependencyComplexity = feature.DependencyComplexity,
            DecisionSpeed = feature.DecisionSpeed,
            ClientTechnicalKnowledge = feature.ClientTechnicalKnowledge,
            DeviceTestCoverage = feature.DeviceTestCoverage,
            TestAutomation = feature.TestAutomation,
            Regression = feature.Regression,
            MiddlewareAvailability = feature.MiddlewareAvailability,
            PaymentProviderIntegration = feature.PaymentProviderIntegration,
            FidoIntegration = feature.FidoIntegration,
            DataMigration = feature.DataMigration,

            // Metrics
            NoOfLanguages = feature.NoOfLanguages,
            NoOfRtlLanguages = feature.NoOfRtlLanguages,
            TpsRequired = feature.TpsRequired,
            WarrantyMonths = feature.WarrantyMonths,
            NoOfFunctionalModules = feature.NoOfFunctionalModules,
            NoOfNoneFunctionalModules = feature.NoOfNoneFunctionalModules,

            NoOfUatCycles = feature.NoOfUatCycles,
            CodeTestCoverage = feature.CodeTestCoverage,
            RestIntegrationPoints = feature.RestIntegrationPoints,
            SoapIntegrationPoints = feature.SoapIntegrationPoints,
            Iso8583IntegrationPoints = feature.Iso8583IntegrationPoints,
            SdkIntegrationPoints = feature.SdkIntegrationPoints,
        };

        return (project, featureData);
    }
    
    //Project Model to Response Detail DTO
    public static PredictionResponseDetailsDto ToResponseDetailsDto(
        Project project, FeatureData featureData)
    {
        return new PredictionResponseDetailsDto
        {
            
            ProjectId = project.ProjectId,
            ProjectName = project.ProjectName,
            Region = project.Region,
            CalculationStatus = project.CalculationStatus,
            FeatureData = new FeatureDataResponseDto
            {
                FeatureSetId = featureData.FeatureSetId,
                DevEnvironment = featureData.DevEnvironment,
                SitEnvironment = featureData.SitEnvironment,
                UatEnvironment = featureData.UatEnvironment,
                StagingEnvironment = featureData.StagingEnvironment,
                TrainingEnvironment = featureData.TrainingEnvironment,
                ProductionEnvironment = featureData.ProductionEnvironment,
                DrEnvironment = featureData.DrEnvironment,

                CompliancePciSff = featureData.CompliancePciSff,
                CountrySpecificCompliance = featureData.CountrySpecificCompliance,

                BackendType = featureData.BackendType,
                FrontendType = featureData.FrontendType,
                MobileType = featureData.MobileType,
                DatabaseType = featureData.DatabaseType,

                GoogleSso = featureData.GoogleSso,
                AppleSso = featureData.AppleSso,
                FacebookSso = featureData.FacebookSso,
                IamVendor = featureData.IamVendor,

                InfraProvider = featureData.InfraProvider,

                DependencyComplexity = featureData.DependencyComplexity,
                DecisionSpeed = featureData.DecisionSpeed,
                ClientTechnicalKnowledge = featureData.ClientTechnicalKnowledge,
                DeviceTestCoverage = featureData.DeviceTestCoverage,
                TestAutomation = featureData.TestAutomation,
                Regression = featureData.Regression,
                MiddlewareAvailability = featureData.MiddlewareAvailability,
                PaymentProviderIntegration = featureData.PaymentProviderIntegration,
                FidoIntegration = featureData.FidoIntegration,
                DataMigration = featureData.DataMigration,

                NoOfLanguages = featureData.NoOfLanguages,
                NoOfRtlLanguages = featureData.NoOfRtlLanguages,
                TpsRequired = featureData.TpsRequired,
                WarrantyMonths = featureData.WarrantyMonths,
                NoOfFunctionalModules = featureData.NoOfFunctionalModules,
                NoOfNoneFunctionalModules = featureData.NoOfNoneFunctionalModules,

                NoOfUatCycles = featureData.NoOfUatCycles,
                CodeTestCoverage = featureData.CodeTestCoverage,
                RestIntegrationPoints = featureData.RestIntegrationPoints,
                SoapIntegrationPoints = featureData.SoapIntegrationPoints,
                Iso8583IntegrationPoints = featureData.Iso8583IntegrationPoints,
                SdkIntegrationPoints = featureData.SdkIntegrationPoints,
            }
        };
    }

    //Input Form View Model to Save DTO
    public static PredictionRequestSaveDto ViewModelToSaveDto(
        PredictionInputViewModel vm)
    {
        return new PredictionRequestSaveDto
        {
            ProjectName = vm.ProjectName,
            Region = vm.Region ?? Region.Gcc,
            CalculationStatus = CalculationStatusType.Pending,

            FeatureData = new FeatureDataSaveRequestDto
            {
                // Environments
                DevEnvironment = vm.DevEnvironment,
                SitEnvironment = vm.SitEnvironment,
                UatEnvironment = vm.UatEnvironment,
                StagingEnvironment = vm.StagingEnvironment,
                TrainingEnvironment = vm.TrainingEnvironment,
                ProductionEnvironment = vm.ProductionEnvironment, // If not included in VM, set default
                DrEnvironment = vm.DrEnvironment, // Same here

                // Compliance
                CompliancePciSff = vm.CompliancePciSff, // default if not in VM
                CountrySpecificCompliance = vm.CountrySpecificCompliance,

                // Technology
                BackendType = vm.BackendType,
                FrontendType = vm.FrontendType,
                MobileType = vm.MobileType,
                DatabaseType = vm.DatabaseType,

                // SSO
                GoogleSso = vm.GoogleSso,
                AppleSso = vm.AppleSso,
                FacebookSso = vm.FacebookSso,
                IamVendor = vm.IamVendor,

                // Infra
                InfraProvider = vm.InfraProvider,

                // Ratings
                DependencyComplexity = vm.DependencyComplexity,
                DecisionSpeed = vm.DecisionSpeed,
                ClientTechnicalKnowledge = vm.ClientTechnicalKnowledge,
                DeviceTestCoverage = vm.DeviceTestCoverage,
                TestAutomation = vm.TestAutomation,
                Regression = vm.Regression,
                MiddlewareAvailability = vm.MiddlewareAvailability,
                PaymentProviderIntegration = vm.PaymentProviderIntegration,
                FidoIntegration = vm.FidoIntegration,
                DataMigration = vm.DataMigration,

                // Metrics
                NoOfLanguages = vm.NoOfLanguages,
                NoOfRtlLanguages = vm.NoOfRtlLanguages,
                TpsRequired = vm.TpsRequired,
                WarrantyMonths = vm.WarrantyMonths,
                NoOfFunctionalModules = vm.NoOfFunctionalModules,
                NoOfNoneFunctionalModules = vm.NoOfNoneFunctionalModules,

                NoOfUatCycles = vm.NoOfUatCycles,
                CodeTestCoverage = vm.CodeTestCoverage,
                RestIntegrationPoints = vm.RestIntegrationPoints,
                SoapIntegrationPoints = vm.SoapIntegrationPoints,
                Iso8583IntegrationPoints = vm.Iso8583IntegrationPoints,
                SdkIntegrationPoints = vm.SdkIntegrationPoints,
            }
        };
    }
}