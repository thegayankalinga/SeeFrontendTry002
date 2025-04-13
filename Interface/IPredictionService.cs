using SeeFrontendTry002.Dtos;

namespace SeeFrontendTry002.Interface;

public interface IPredictionService
{
    Task<PredictionApiResponseDto?> PredictAsync(PredictionApiRequestDto apiRequest);

}