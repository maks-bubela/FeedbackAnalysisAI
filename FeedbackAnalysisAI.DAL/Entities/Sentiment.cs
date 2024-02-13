using FeedbackAnalysisAI.DAL.Interfaces;

namespace FeedbackAnalysisAI.DAL.Entities
{
    public class Sentiment : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; } = new HashSet<Feedback>();
    }
}
