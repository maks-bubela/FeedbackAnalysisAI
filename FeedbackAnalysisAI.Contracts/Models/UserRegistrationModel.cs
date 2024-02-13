using System.ComponentModel.DataAnnotations;

namespace FeedbackAnalysisAI.Contracts.Models
{
    public class UserRegistrationModel
    {
        /// <summary>
        /// User username
        /// </summary>
        [Required, MinLength(4), MaxLength(36)]
        public string Username { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        [Required, MinLength(6), MaxLength(36), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, MinLength(6), MaxLength(36), Compare("Password", ErrorMessage = "Password dont match")]
        public string RePassword { get; set; }
        /// <summary>
        /// User first name
        /// </summary>
        [Required, MinLength(3), MaxLength(36)]
        public string Firstname { get; set; }

        /// <summary>
        /// User last name
        /// </summary>
        [Required, MinLength(3), MaxLength(36)]
        public string Lastname { get; set; }

        /// <summary>
        /// User Email
        /// </summary>
        [Required, MinLength(3), EmailAddress]
        public string Email { get; set; }
    }
}
