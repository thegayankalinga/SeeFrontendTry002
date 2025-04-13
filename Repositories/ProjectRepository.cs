using Microsoft.EntityFrameworkCore;
using SeeFrontendTry002.Data;
using SeeFrontendTry002.Dtos;
using SeeFrontendTry002.Interface;
using SeeFrontendTry002.Mappers;
using SeeFrontendTry002.Models;

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

}