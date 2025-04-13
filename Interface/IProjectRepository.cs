using SeeFrontendTry002.Dtos;
using SeeFrontendTry002.Models;

namespace SeeFrontendTry002.Interface;

public interface IProjectRepository
{
    Task<PredictionResponseDetailsDto?> SaveProjectWithFeaturesAsync(PredictionRequestSaveDto dto);
    Task<PredictionResponseDetailsDto?> GetProjectWithFeatureByIdAsync(int projectId);
    Task<IEnumerable<PredictionResponseDetailsDto>> GetAllProjectsWithFeaturesAsync();
}