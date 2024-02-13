using FeedbackAnalysisAI.Contracts.Enums;

namespace FeedbackAnalysisAI.Contracts.Services
{
    public interface ITokenService
    {
        Task<int> GetTokenSettingsAsync(EnvirementTypes type);
    }
}
