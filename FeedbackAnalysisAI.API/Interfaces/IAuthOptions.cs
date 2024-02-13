using FeedbackAnalysisAI.Contracts.DTO;


namespace FeedbackAnalysisAI.API.Interfaces
{
    public interface IAuthOptions
    {
        string GetSymmetricSecurityKey(TokenSettingsDTO settingsDto);
    }
}
