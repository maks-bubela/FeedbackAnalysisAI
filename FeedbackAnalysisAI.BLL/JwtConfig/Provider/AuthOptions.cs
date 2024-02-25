using FeedbackAnalysisAI.Contracts.DTO;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FeedbackAnalysisAI.Contracts.Services;
using FeedbackAnalysisAI.BLL.JwtConfig.JwtSettings;

namespace FeedbackAnalysisAI.BLL.JwtConfig.Provider
{
    public class AuthOptions : IAuthOptions
    {
        private readonly IOptions<JwtSetting> _config;

        public AuthOptions(IOptions<JwtSetting> config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public TokensDTO GetJWTTokens(TokenSettingsDTO settingsDto)
        {
            var now = DateTime.UtcNow;
            var jwtConfig = _config.Value;
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.Key));
            var jwt = new JwtSecurityToken(
                issuer: jwtConfig.Issuer,
                audience: jwtConfig.Audience,
                notBefore: now,
                claims: settingsDto.Identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(settingsDto.LifeTime)),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));
            var tokens = new TokensDTO()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt),
                RefreshToken = GenerateRefreshToken()
            };
            return tokens;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var jwtConfig = _config.Value;
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                RequireExpirationTime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.Key)),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException();
            }

            return principal;
        }
    }
}
