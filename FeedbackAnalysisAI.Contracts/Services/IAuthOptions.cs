using FeedbackAnalysisAI.Contracts.DTO;
using FeedbackAnalysisAI.Contracts.Models;
using System.Security.Claims;


namespace FeedbackAnalysisAI.Contracts.Services
{
    public interface IAuthOptions
    {
        TokensDTO GetJWTTokens(TokenSettingsDTO settingsDto);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        string GenerateRefreshToken();
    }
}
