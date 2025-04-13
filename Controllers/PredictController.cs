using Microsoft.AspNetCore.Mvc;
using SeeFrontendTry002.Dtos;
using SeeFrontendTry002.Interface;
using SeeFrontendTry002.Mappers;
using SeeFrontendTry002.Models;
using SeeFrontendTry002.Models.Enumz;
using SeeFrontendTry002.Services;
using SeeFrontendTry002.ViewModels;

namespace SeeFrontendTry002.Controllers;

public class PredictController : Controller
{
    private readonly IPredictionService _predictionService;
    private readonly IAppUserRepository _appUserRepository;
    private readonly ILogger<PredictController> _logger;
    private readonly IProjectRepository _projectRepository;

    public PredictController(
        IPredictionService predictionService,
        IAppUserRepository appUserRepository,
        ILogger<PredictController> logger,
        IProjectRepository projectRepository
        )
    {
        _predictionService = predictionService;
        _appUserRepository = appUserRepository;
        _logger = logger;
        _projectRepository = projectRepository;
    }
    // GET
    public IActionResult Index()
    {
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Predict(PredictionInputViewModel predictionInputViewModel)
    {
        #region GetTheUserID
        //Get the UserIds for the following Emails.
        var preSalesUserId = await _appUserRepository.GetUserIdByEmailAsync("pse001@see.com");
        var pmUserId = await _appUserRepository.GetUserIdByEmailAsync(" pm1@see.com");
        if (preSalesUserId is null || pmUserId is null)
        {
            if (preSalesUserId is null)
                _logger.LogError("PreSales user not found for email: pse001@see.com");

            if (pmUserId is null)
                _logger.LogError("Project Manager user not found for email: pm1@see.com");

            return BadRequest("One or more required users (PreSales or Project Manager) were not found.");
        }

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
        
        if (apiPredictionResultDto == null)
        {
            return View("Error");
        }

        return View("PredictionResult", apiPredictionResultDto);

    }
}