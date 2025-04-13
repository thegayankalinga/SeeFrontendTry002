using SeeFrontendTry002.Models;
using SeeFrontendTry002.Models.Enumz;

namespace SeeFrontendTry002.Dtos;

public class FeatureDataResponseDto
{
    //One to One Data (Feature Data
    public int FeatureSetId { get; set; }
    
    //Environments
    public bool DevEnvironment { get; set; }
    public bool SitEnvironment { get; set; }
    public bool UatEnvironment { get; set; }
    public bool StagingEnvironment { get; set; }
    public bool TrainingEnvironment { get; set; }
    public bool ProductionEnvironment { get; set; }
    public bool DrEnvironment {get; set;}
    
    //Compliance
    public bool CompliancePciSff { get; set; }
    public bool CountrySpecificCompliance { get; set; }
    
    //Technology
    public BackendType BackendType { get; set; }
    public FrontendType FrontendType { get; set; }
    public MobileType MobileType { get; set; }
    public DatabaseType DatabaseType { get; set; }
    
    //SSO
    public bool GoogleSso  { get; set; }
    public bool AppleSso { get; set; }
    public bool FacebookSso { get; set; }
    public IamVendorType IamVendor { get; set; }
    
    //Infra
    public InfraType InfraProvider { get; set; }
    
    public RatingType DependencyComplexity { get; set; }
    public SpeedType DecisionSpeed { get; set; }
    public RatingType ClientTechnicalKnowledge { get; set; }
    public RatingType DeviceTestCoverage { get; set; }
    public bool TestAutomation { get; set; }
    public RegressionType Regression { get; set; }
    public bool MiddlewareAvailability { get; set; }
    public bool PaymentProviderIntegration { get; set; }
    public bool FidoIntegration { get; set; }
    public bool DataMigration { get; set; }
    
    public int NoOfLanguages { get; set; }
    public int NoOfRtlLanguages { get; set; }
    public int TpsRequired { get; set; }
    public int WarrantyMonths { get; set; }
    public int NoOfFunctionalModules { get; set; }
    public int NoOfNoneFunctionalModules { get; set; }
    
    public int NoOfUatCycles { get; set; }
    public float CodeTestCoverage { get; set; }
    public int RestIntegrationPoints { get; set; }
    public int SoapIntegrationPoints { get; set; }
    public int Iso8583IntegrationPoints { get; set; }
    public int SdkIntegrationPoints { get; set; }
    
    //One to many
    public ICollection<PredictionResult>? PredictionResultsFromFeatureData { get; set; }
}