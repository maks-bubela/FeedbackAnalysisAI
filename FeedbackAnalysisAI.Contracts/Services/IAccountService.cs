using FeedbackAnalysisAI.Contracts.DTO;

namespace FeedbackAnalysisAI.Contracts.Services
{
    public interface IAccountService
    {
        Task<long> RegisterUserAsync(UserDTO userDTO);
        Task<bool> VerifyCredentialsAsync(string username, string password);
    }
}
