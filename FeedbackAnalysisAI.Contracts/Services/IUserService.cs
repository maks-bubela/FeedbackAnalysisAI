using FeedbackAnalysisAI.Contracts.DTO;

namespace FeedbackAnalysisAI.Contracts.Services
{
    public interface IUserService
    {
        Task<bool> UserExistsAsync(string username);
        Task<bool> UserExistsAsync(long id);
        Task<UserDTO> GetUserByIdAsync(long id);
        Task<UserDTO> GetUserByUsernameAsync(string username);
    }
}
