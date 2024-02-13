using System.Collections.Generic;

namespace FeedbackAnalysisAI.Contracts.Models
{
    public class CurrentUserModel
    {
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public Dictionary<string, string> Claims { get; set; }
    }
}