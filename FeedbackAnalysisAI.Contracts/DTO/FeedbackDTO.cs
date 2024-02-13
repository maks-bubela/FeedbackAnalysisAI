namespace FeedbackAnalysisAI.Contracts.DTO
{
    public class FeedbackDTO
    {
        public string Text { get; set; }
        public string? SentimentName { get; set; }
        public long UserId { get; set; }
    }
}
