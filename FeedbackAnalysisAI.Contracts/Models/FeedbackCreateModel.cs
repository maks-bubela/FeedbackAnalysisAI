using System.ComponentModel.DataAnnotations;

namespace FeedbackAnalysisAI.Contracts.Models
{
    public class FeedbackCreateModel
    {
        /// <summary>
        /// Feedback text.
        /// </summary>
        [Required, MinLength(20), MaxLength(2000)]
        public string Text { get; set; }
    }
}
