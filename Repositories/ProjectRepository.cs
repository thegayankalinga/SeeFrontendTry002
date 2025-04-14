using Microsoft.EntityFrameworkCore;
using SeeFrontendTry002.Data;
using SeeFrontendTry002.Dtos;
using SeeFrontendTry002.Interface;
using SeeFrontendTry002.Mappers;
using SeeFrontendTry002.Models;
using SeeFrontendTry002.Models.Enumz;

namespace SeeFrontendTry002.Repositories;

public class ProjectRepository: IProjectRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProjectRepository> _logger;
    
    public ProjectRepository(ApplicationDbContext context, ILogger<ProjectRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> UpdateCalculationStatusAsync(int projectId, CalculationStatusType status)
    {
        var project = await _context.Projects.FindAsync(projectId);
        
        if (project is null)
        {
            return false;
        }
        
        // Update the status
        project.CalculationStatus = status;

        // Mark entity as modified (optional, EF should detect this)
        _context.Projects.Update(project);

        // Save changes to the database
        await _context.SaveChangesAsync();

        return true;
    }
    
    
    public async Task<PredictionResponseDetailsDto?> SaveProjectWithFeaturesAsync(
        PredictionRequestSaveDto dto)
    {
        try
        {
            var (project, featureData) = ProjectMapper.ToEntities(dto);
            project.FeatureData = featureData;
            
            
            _context.Projects.Add(project);
            
            // Tracking EF Changes (for debugging or logging)
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                _logger.LogInformation($"{entry.Entity.GetType().Name}: {entry.State}");
            }
            
            await _context.SaveChangesAsync(); // IDs now populated

            return ProjectMapper.ToResponseDetailsDto(project, featureData); // return mapped result
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving project and feature data.");
            return null;
        }
    }
    
    public async Task<bool> SavePredictionResultAsync(int projectId, int featureSetId, PredictionModel predictionModel, PredictionApiResponseDto dto)
    {
        try
        {
            var result = new PredictionResult
            {
                ProjectId = projectId,
                FeatureSetId = featureSetId,
                ModelName = predictionModel,
                DeliveryEffort = dto.Predictions.DeliveryEffort,
                EngineeringEffort = dto.Predictions.EngineeringEffort,
                DevOpsEffort = dto.Predictions.DevOpsEffort,
                QaEffort = dto.Predictions.QaEffort
            };
            _context.PredictionResults.Add(result);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving prediction result.");
            return false;
        }
    }
    
    public async Task<PredictionResponseDetailsDto?> GetProjectWithFeatureByIdAsync(int projectId)
    {
        var project = await _context.Projects
            .Include(p => p.FeatureData)
            .FirstOrDefaultAsync(p => p.ProjectId == projectId);

        if (project == null || project.FeatureData == null)
            return null;

        return ProjectMapper.ToResponseDetailsDto(project, project.FeatureData);
    }
    
    public async Task<IEnumerable<PredictionResponseDetailsDto>> GetAllProjectsWithFeaturesAsync()
    {
        var projects = await _context.Projects
            .Include(p => p.FeatureData)
            .ToListAsync();

        var result = projects
            .Where(p => p.FeatureData != null)
            .Select(p => ProjectMapper.ToResponseDetailsDto(p, p.FeatureData!))
            .ToList();

        return result;
    }

    public async Task<List<PredictionResultResponseDto>> GetPredictedResulstsByProjectIdAsync(int projectId)
    {
        var results = await _context.PredictionResults.Where(p => p.ProjectId == projectId).AsNoTracking().ToListAsync();
        
       
        var dtos= results.Select(r => ProjectMapper.ResultModelToResultResponseDto(r)).ToList();

        return dtos;
    }

    public async Task<bool> DeleteProjectAsync(int projectId)
    {
        // Start a transaction to ensure all related data is deleted
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Get the project
            var project = await _context.Projects
                .Include(p => p.FeatureData)
                .Include(p => p.PredictionResults)
                .FirstOrDefaultAsync(p => p.ProjectId == projectId);
                
            if (project == null)
                return false;
                
            // Remove prediction results first (to avoid foreign key constraints)
            if (project.PredictionResults != null && project.PredictionResults.Any())
            {
                _context.PredictionResults.RemoveRange(project.PredictionResults);
            }
            
            // Remove feature data
            if (project.FeatureData != null)
            {
                _context.FeatureData.Remove(project.FeatureData);
            }
            
            // Finally remove the project
            _context.Projects.Remove(project);
            
            // Save changes
            await _context.SaveChangesAsync();
            
            // Commit the transaction
            await transaction.CommitAsync();
            
            return true;
        }
        catch (Exception)
        {
            // Rollback the transaction if anything fails
            await transaction.RollbackAsync();
            return false;
        }
    }
    
    public async Task<bool> DeletePredictionResultAsync(int resultId)
    {
        try
        {
            // Find the prediction result
            var result = await _context.PredictionResults.FindAsync(resultId);
            
            if (result == null)
                return false;
                
            // Remove the prediction result
            _context.PredictionResults.Remove(result);
            
            // Save changes
            await _context.SaveChangesAsync();
            
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
}