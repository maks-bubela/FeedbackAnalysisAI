using FeedbackAnalysisAI.DAL.Interfaces;

namespace FeedbackAnalysisAI.DAL.Entities
{
    public class Role : IEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<User> Users { get; private set; } = new HashSet<User>();
    }
}
