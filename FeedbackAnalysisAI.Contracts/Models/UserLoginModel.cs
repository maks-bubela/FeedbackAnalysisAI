using System.ComponentModel.DataAnnotations;

namespace FeedbackAnalysisAI.Contracts.Models
{
    public class UserLoginModel
    {
        /// <summary>
        /// User's username.
        /// </summary>
        [Required, MinLength(4), MaxLength(16)]
        public string Username { get; set; }

        /// <summary>
        /// User's password.
        /// </summary>
        [Required, MinLength(6), MaxLength(16)]
        public string Password { get; set; }
    }
}
