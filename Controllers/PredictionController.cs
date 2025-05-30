using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SeeFrontendTry002.Dtos;
using SeeFrontendTry002.Interface;
using SeeFrontendTry002.Mappers;
using SeeFrontendTry002.Models;
using SeeFrontendTry002.Models.Enumz;
using SeeFrontendTry002.Services;
using SeeFrontendTry002.ViewModels;

namespace SeeFrontendTry002.Controllers;

public class PredictionController : Controller
{
    private readonly IPredictionService _predictionService;
    private readonly IAppUserRepository _appUserRepository;
    private readonly ILogger<PredictionController> _logger;
    private readonly IProjectRepository _projectRepository;

    public PredictionController(
        IPredictionService predictionService,
        IAppUserRepository appUserRepository,
        ILogger<PredictionController> logger,
        IProjectRepository projectRepository
        )
    {
        _predictionService = predictionService;
        _appUserRepository = appUserRepository;
        _logger = logger;
        _projectRepository = projectRepository;
    }
    // GET
    public async Task<IActionResult> Index(string? searchKey)
    {
        var projects = await _projectRepository.GetAllProjectsWithFeaturesAsync();
        ViewData["SearchKey"] = searchKey;
        return View(projects);
    }

    [HttpPost]
    public async Task<IActionResult> PredictExisting(int projectId, PredictionModel predictionModel)
    {
        var project = await _projectRepository.GetProjectWithFeatureByIdAsync(projectId);
        if (project == null)
        {
            _logger.LogWarning("Project {projectId} does not exist to predict", projectId);
            return RedirectToAction("Error", "Home");
        }
        
        var apiRequestDto = PredictionApiRequestMapper.ResponseDetailsToApiRequest(project, predictionModel);
        
        var apiPredictionResultDto = await _predictionService.PredictAsync(apiRequestDto);
        
        if (apiPredictionResultDto is null)
        {   
            await _projectRepository.UpdateCalculationStatusAsync(project.ProjectId, CalculationStatusType.Failed);
            _logger.LogError($"Project {project.ProjectName} Was null.");
            return RedirectToAction("Error", "Home");
        }
        _logger.LogInformation("Prediction results - Delivery: {Delivery}, Engineering: {Engineering}, DevOps: {DevOps}, QA: {QA}",
            apiPredictionResultDto.Predictions.DeliveryEffort,
            apiPredictionResultDto.Predictions.EngineeringEffort,
            apiPredictionResultDto.Predictions.DevOpsEffort,
            apiPredictionResultDto.Predictions.QaEffort);

        
        await _projectRepository.SavePredictionResultAsync(
                project.ProjectId, 
                project.FeatureData.FeatureSetId,
                predictionModel,
                apiPredictionResultDto);
        
        var updateResult = await _projectRepository.UpdateCalculationStatusAsync(
                            project.ProjectId, 
                            CalculationStatusType.Success);

        if (!updateResult)
        {
            _logger.LogError($"Project {project.ProjectName} could not be updated with the status.");
            return RedirectToAction("Error", "Home");
        }
        
        var result = await _projectRepository.GetPredictedResulstsByProjectIdAsync(projectId);
        
        var vm = new ResultViewModel
        {
            ProjectId = projectId,
            FeaturesetId = project.FeatureData.FeatureSetId,
            PredictionInputViewModel = ProjectMapper.ToViewModel(project),
            ProjectName = project.ProjectName,
            Results = result,
            Success = true
        };
        return View("Results", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Predict(PredictionInputViewModel predictionInputViewModel)
    {
        #region GetTheUserID
        //Get the UserIds for the following Emails.
        // var preSalesUserId = await _appUserRepository.GetUserIdByEmailAsync("pse001@see.com");
        // var pmUserId = await _appUserRepository.GetUserIdByEmailAsync(" pm1@see.com");
        // if (preSalesUserId is null || pmUserId is null)
        // {
        //     if (preSalesUserId is null)
        //         _logger.LogError("PreSales user not found for email: pse001@see.com");
        //
        //     if (pmUserId is null)
        //         _logger.LogError("Project Manager user not found for email: pm1@see.com");
        //
        //     return BadRequest("One or more required users (PreSales or Project Manager) were not found.");
        // }

        #endregion
        //Map the View Model to the PredictionResponseDetailsDto
        
        var saveDto = ProjectMapper.ViewModelToSaveDto(predictionInputViewModel);
        //Save the record
        var savedResult = await _projectRepository.SaveProjectWithFeaturesAsync(saveDto);

        if (savedResult is null)
        {
            _logger.LogError($"Project {saveDto.ProjectName} could not be saved.");
            return RedirectToAction("Index", "Home");
        }
        //Get the record back and map to API
        
        //Pass the detailResponseDto & Model from the Viewmodel to the below function
        var apiRequestDto = PredictionApiRequestMapper.ResponseDetailsToApiRequest(savedResult, predictionInputViewModel.PredictionModelName);
        
        var apiPredictionResultDto = await _predictionService.PredictAsync(apiRequestDto);

        bool updateResult = false;
        if (apiPredictionResultDto is null)
        {   
            updateResult =
                await _projectRepository.UpdateCalculationStatusAsync(savedResult.ProjectId, CalculationStatusType.Failed);
            _logger.LogError($"Project {saveDto.ProjectName} Was null.");
            return View("Error");
        }
        _logger.LogInformation("Prediction results - Delivery: {Delivery}, Engineering: {Engineering}, DevOps: {DevOps}, QA: {QA}",
            apiPredictionResultDto.Predictions.DeliveryEffort,
            apiPredictionResultDto.Predictions.EngineeringEffort,
            apiPredictionResultDto.Predictions.DevOpsEffort,
            apiPredictionResultDto.Predictions.QaEffort);
        
        var savePredictionResult = await _projectRepository.SavePredictionResultAsync(savedResult.ProjectId, savedResult.FeatureData.FeatureSetId, predictionInputViewModel.PredictionModelName, apiPredictionResultDto);
        
        
        if (savePredictionResult is false)
        {
            updateResult =
                await _projectRepository.UpdateCalculationStatusAsync(savedResult.ProjectId, CalculationStatusType.Failed);
            _logger.LogError($"Project {saveDto.ProjectName} could not be saved.");
            return View("Error");
        }
        
        updateResult =
            await _projectRepository.UpdateCalculationStatusAsync(savedResult.ProjectId, CalculationStatusType.Success);

        if (!updateResult)
        {
            _logger.LogError($"Project {saveDto.ProjectName} could not be updated with the status.");
            return View("Error");
        }
        return View("PredictionResult", apiPredictionResultDto);

    }

    public async Task<IActionResult> Detail(int id)
    {
        var project = await _projectRepository.GetProjectWithFeatureByIdAsync(id);
        if (project is null)
        {
            _logger.LogError($"Project {id} could not be found.");
            return RedirectToAction("Error", "Home");
        }

        var viewModel = ProjectMapper.ToViewModel(project);
        return View(viewModel);
    }

    public async Task<IActionResult> Results(int projectId, int featureId)
    {
        try
        {
            var result = await _projectRepository.GetPredictedResulstsByProjectIdAsync(projectId);
            var project = await _projectRepository.GetProjectWithFeatureByIdAsync(projectId);

            if (project is null)
            {
                _logger.LogError($"Project {projectId} could not be found.");
                return RedirectToAction("Error", "Home");
            }
            
            
            
            var vm = new ResultViewModel
            {
                ProjectId = projectId,
                FeaturesetId = featureId,
                PredictionInputViewModel = ProjectMapper.ToViewModel(project),
                ProjectName = project.ProjectName,
                Results = result,
                Success = true
            };
            
            return View(vm);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Project {projectId} could not be found., {ex}");
            return RedirectToAction("Error", "Home");
        }
        
    }
    
    // Delete a project
    [HttpPost]
    public async Task<IActionResult> DeleteProject(int id)
    {
        try
        {
            var result = await _projectRepository.DeleteProjectAsync(id);
        
            if (result)
            {
                // If using AJAX, return JSON result
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    return Json(new { success = true, message = "Project deleted successfully" });
                
                // For regular form posts, redirect with success message
                TempData["SuccessMessage"] = "Project deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // If using AJAX, return JSON error
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    return Json(new { success = false, message = "Failed to delete project" });
                
                // For regular form posts, redirect with error message
                TempData["ErrorMessage"] = "Failed to delete project";
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            // Log the exception
            _logger.LogError(ex, "Error deleting project with ID {ProjectId}", id);
        
            // If using AJAX, return JSON error
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return Json(new { success = false, message = "An error occurred while deleting the project" });
            
            // For regular form posts, redirect with error message
            TempData["ErrorMessage"] = "An error occurred while deleting the project";
            return RedirectToAction(nameof(Index));
        }
    }
    
    // Delete a prediction result
    [HttpPost]
    public async Task<IActionResult> DeletePredictionResult(int resultId, int projectId, int featureId)
    {
        try
        {
            var result = await _projectRepository.DeletePredictionResultAsync(resultId);
            
            if (result)
            {
                // If using AJAX, return JSON result
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    return Json(new { success = true, message = "Prediction result deleted successfully" });
                    
                // For regular form posts, redirect with success message
                TempData["SuccessMessage"] = "Prediction result deleted successfully";
                return RedirectToAction(nameof(Results), new { projectId = projectId, featureId = featureId });
            }
            else
            {
                // If using AJAX, return JSON error
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    return Json(new { success = false, message = "Failed to delete prediction result" });
                    
                // For regular form posts, redirect with error message
                TempData["ErrorMessage"] = "Failed to delete prediction result";
                return RedirectToAction(nameof(Results), new { projectId = projectId, featureId = featureId });
            }
        }
        catch (Exception ex)
        {
            // Log the exception
            _logger.LogError(ex, "Error deleting prediction result with ID {ResultId}", resultId);
            
            // If using AJAX, return JSON error
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return Json(new { success = false, message = "An error occurred while deleting the prediction result" });
                
            // For regular form posts, redirect with error message
            TempData["ErrorMessage"] = "An error occurred while deleting the prediction result";
            return RedirectToAction(nameof(Results), new { projectId = projectId, featureId = featureId });
        }
    }
    
    #region Stepper
        
    #region Step1
    [HttpGet]
    public IActionResult Step1()
    {
        var model = new PredictionInputViewModel();
        return View(model);
    }
    
    [HttpPost]
    public IActionResult Step1(PredictionInputViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        TempData["Step1"] = JsonSerializer.Serialize(model);
        return RedirectToAction("Step2");
    }
    #endregion
    
    #region Step2
    [HttpGet]
    public IActionResult Step2()
    {
        var step1Data = TempData["Step1"]?.ToString();
        if (step1Data == null) return RedirectToAction("Step1");

        var model = JsonSerializer.Deserialize<PredictionInputViewModel>(step1Data)!;
        TempData.Keep("Step1"); // Keep Step1 data alive
        return View(model);
    }
    
    [HttpPost]
    public IActionResult Step2(PredictionInputViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        TempData["Step2"] = JsonSerializer.Serialize(model);
        TempData.Keep("Step1"); // Retain Step1 for next step
        return RedirectToAction("Step3");
    }

    #endregion
    
    #region Step3
    [HttpGet]
    public IActionResult Step3()
    {
        var step1Data = TempData["Step1"]?.ToString();
        var step2Data = TempData["Step2"]?.ToString();

        if (step1Data == null || step2Data == null)
            return RedirectToAction("Step1");

        // Merge Step1 & Step2 data
        var step1Model = JsonSerializer.Deserialize<PredictionInputViewModel>(step1Data)!;
        var step2Model = JsonSerializer.Deserialize<PredictionInputViewModel>(step2Data)!;

        // copy values from step2Model into step1Model
        step1Model.BackendType = step2Model.BackendType;
        step1Model.FrontendType = step2Model.FrontendType;
        step1Model.MobileType = step2Model.MobileType;
        step1Model.DatabaseType = step2Model.DatabaseType;
        step1Model.GoogleSso = step2Model.GoogleSso;
        step1Model.AppleSso = step2Model.AppleSso;
        step1Model.FacebookSso = step2Model.FacebookSso;
        step1Model.CompliancePciSff = step2Model.CompliancePciSff;
        step1Model.CountrySpecificCompliance = step2Model.CountrySpecificCompliance;
        step1Model.IamVendor = step2Model.IamVendor;
        step1Model.InfraProvider = step2Model.InfraProvider;

        TempData["Step1"] = JsonSerializer.Serialize(step1Model); // merged data
        TempData.Keep("Step1");

        return View(step1Model);
    }

    [HttpPost]
    public IActionResult Step3(PredictionInputViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        TempData["Step3"] = JsonSerializer.Serialize(model);
        TempData.Keep("Step1");

        return RedirectToAction("Step4");
    }
    #endregion
    
    #region Step4
    [HttpGet]
    public IActionResult Step4()
    {
        var step1Data = TempData["Step1"]?.ToString();
        var step3Data = TempData["Step3"]?.ToString();

        if (step1Data == null || step3Data == null)
            return RedirectToAction("Step1");

        var step1Model = JsonSerializer.Deserialize<PredictionInputViewModel>(step1Data)!;
        var step3Model = JsonSerializer.Deserialize<PredictionInputViewModel>(step3Data)!;

        // Merge values from Step 3
        step1Model.DependencyComplexity = step3Model.DependencyComplexity;
        step1Model.DecisionSpeed = step3Model.DecisionSpeed;
        step1Model.ClientTechnicalKnowledge = step3Model.ClientTechnicalKnowledge;
        step1Model.DeviceTestCoverage = step3Model.DeviceTestCoverage;
        step1Model.TestAutomation = step3Model.TestAutomation;
        step1Model.Regression = step3Model.Regression;
        step1Model.MiddlewareAvailability = step3Model.MiddlewareAvailability;
        step1Model.PaymentProviderIntegration = step3Model.PaymentProviderIntegration;
        step1Model.FidoIntegration = step3Model.FidoIntegration;
        step1Model.DataMigration = step3Model.DataMigration;
        step1Model.NoOfLanguages = step3Model.NoOfLanguages;
        step1Model.NoOfRtlLanguages = step3Model.NoOfRtlLanguages;

        TempData["Step1"] = JsonSerializer.Serialize(step1Model);
        TempData.Keep("Step1");

        return View(step1Model);
    }

    [HttpPost]
    public IActionResult Step4(PredictionInputViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        TempData["Step4"] = JsonSerializer.Serialize(model);
        TempData.Keep("Step1");
        return RedirectToAction("Step5");
    }
    #endregion
    
    #region Step5
    [HttpGet]
    public IActionResult Step5()
    {
        var step1Data = TempData["Step1"]?.ToString();
        var step4Data = TempData["Step4"]?.ToString();

        if (step1Data == null || step4Data == null)
            return RedirectToAction("Step1");

        var merged = JsonSerializer.Deserialize<PredictionInputViewModel>(step1Data)!;
        var final = JsonSerializer.Deserialize<PredictionInputViewModel>(step4Data)!;

        // Copy Step4-specific fields into merged model
        merged.TpsRequired = final.TpsRequired;
        merged.WarrantyMonths = final.WarrantyMonths;
        merged.NoOfFunctionalModules = final.NoOfFunctionalModules;
        merged.NoOfNoneFunctionalModules = final.NoOfNoneFunctionalModules;
        merged.NoOfUatCycles = final.NoOfUatCycles;
        merged.CodeTestCoverage = final.CodeTestCoverage;
        merged.RestIntegrationPoints = final.RestIntegrationPoints;
        merged.SoapIntegrationPoints = final.SoapIntegrationPoints;
        merged.Iso8583IntegrationPoints = final.Iso8583IntegrationPoints;
        merged.SdkIntegrationPoints = final.SdkIntegrationPoints;

        TempData["FinalModel"] = JsonSerializer.Serialize(merged);
        TempData.Keep("FinalModel");
        return View(merged);
    }
    #endregion
    
    
    #endregion
}