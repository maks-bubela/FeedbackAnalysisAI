using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackAnalysisAI.Contracts.Models
{
    public class FeedbackAndSentiemntModel
    {
        /// <summary>
        /// Feedback text.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Feedback sentiment analisys.
        /// </summary>
        public string SentimentName { get; set; }
    }
}
