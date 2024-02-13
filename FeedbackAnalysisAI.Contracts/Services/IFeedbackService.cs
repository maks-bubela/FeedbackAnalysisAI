using FeedbackAnalysisAI.Contracts.Consts;
using FeedbackAnalysisAI.Contracts.DTO;
namespace FeedbackAnalysisAI.Contracts.Services
{
    public interface IFeedbackService
    {
        Task<FeedbackDTO> GetFeedbackAsync(long feedbackId);
        Task<long> AddFeedbackAsync(FeedbackDTO feedbackDTO);
        Task<List<FeedbackDTO>> GetFeedbacksListAsync();

    }
}
