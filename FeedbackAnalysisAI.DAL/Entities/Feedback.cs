using FeedbackAnalysisAI.DAL.Interfaces;

namespace FeedbackAnalysisAI.DAL.Entities
{
    public class Feedback : IEntity
    {
        public long Id { get; set; }
        public string Text {  get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
        public Sentiment Sentiment { get; set; }
        public long SentimentId { get; set; }


    }
}
