using Azure;
using Azure.AI.TextAnalytics;
using FeedbackAnalysisAI.Contracts.Services;

namespace FeedbackAnalysisAI.AzureService.AiService.Provider
{
    public class AzureAiServiceProvider : IAzureAiServiceProvider
    {
        private readonly TextAnalyticsClient _textAnalyticsClient;

        public AzureAiServiceProvider(Uri endpoint, AzureKeyCredential apiKey)
        {
            _textAnalyticsClient = new TextAnalyticsClient(endpoint, apiKey);
        }

        public async Task<TextSentiment> AnalyzeSentimentAsync(string feedbackText)
        {

            var response = await _textAnalyticsClient.AnalyzeSentimentAsync(feedbackText);

            return response.Value.Sentiment;
        }
    }
}
