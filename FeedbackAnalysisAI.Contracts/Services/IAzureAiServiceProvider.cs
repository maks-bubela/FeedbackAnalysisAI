using Azure.AI.TextAnalytics;

namespace FeedbackAnalysisAI.Contracts.Services
{
    public interface IAzureAiServiceProvider
    {
        Task<TextSentiment> AnalyzeSentimentAsync(string feedbackText);
    }
}
