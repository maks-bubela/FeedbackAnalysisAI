using FeedbackAnalysisAI.DAL.Interfaces;

namespace FeedbackAnalysisAI.DAL.Entities
{
    public class RefreshTokens : IEntity
    {
        public long Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string Fingerprint { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
    }
}
