using FeedbackAnalysisAI.Contracts.DTO;
using FeedbackAnalysisAI.Contracts.Enums;
using System.Security.Claims;

namespace FeedbackAnalysisAI.Contracts.Services
{
    public interface ITokenService
    {
        Task<int> GetTokenSettingsAsync(EnvirementTypes type);
        Task<TokensDTO?> RefreshTokenProccessingAsync(string fingerprint, TokensDTO tokensDTO);
        Task<TokensDTO> AddRefreshTokenAsync(ClaimsIdentity identity, string fingerprint);
    }
}
