using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackAnalysisAI.Contracts.DTO
{
    public class RefreshTokensDTO
    {
        public string Token { get; set; }
        public string Fingerprint { get; set; }
        public long UserId { get; set; }
    }
}
