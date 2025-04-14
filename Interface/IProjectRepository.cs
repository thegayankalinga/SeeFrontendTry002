using SeeFrontendTry002.Dtos;
using SeeFrontendTry002.Models;
using SeeFrontendTry002.Models.Enumz;

namespace SeeFrontendTry002.Interface;

public interface IProjectRepository
{
    Task<PredictionResponseDetailsDto?> SaveProjectWithFeaturesAsync(PredictionRequestSaveDto dto);
    Task<PredictionResponseDetailsDto?> GetProjectWithFeatureByIdAsync(int projectId);
    Task<IEnumerable<PredictionResponseDetailsDto>> GetAllProjectsWithFeaturesAsync();

    Task<bool> SavePredictionResultAsync(int projectId, int featureSetId, PredictionModel predictionModel,
        PredictionApiResponseDto dto);

    Task<bool> UpdateCalculationStatusAsync(int projectId, CalculationStatusType status);
}