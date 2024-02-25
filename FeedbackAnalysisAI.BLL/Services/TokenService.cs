using AutoMapper;
using FeedbackAnalysisAI.BLL.Exceptions;
using FeedbackAnalysisAI.Contracts.Consts;
using FeedbackAnalysisAI.Contracts.DTO;
using FeedbackAnalysisAI.Contracts.Enums;
using FeedbackAnalysisAI.Contracts.Services;
using FeedbackAnalysisAI.DAL.Entities;
using FeedbackAnalysisAI.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Principal;


namespace FeedbackAnalysisAI.BLL.Services
{
    public class TokenService : ITokenService
    {
        #region Repository
        private readonly IGenericRepository _repository;
        #endregion

        #region Services
        private readonly IMapper _mapper;
        private readonly IAuthOptions _authOptions;
        #endregion

        #region Enums
        private readonly EnvirementTypes _envirementType;
        #endregion

        #region Predicates
        private Expression<Func<BearerTokenSetting, bool>> GetTokenSettingsPredicate(EnvirementTypes type)
        {
            Expression<Func<BearerTokenSetting, bool>> predicate = b => b.EnvironmentType.Id == ((long)type);
            return predicate;
        }
        private Expression<Func<RefreshTokens, bool>> GetRefreshTokenPredicate(RefreshTokensDTO refreshTokensDTO)
        {
            Expression<Func<RefreshTokens, bool>> predicate = b => 
            b.Token == refreshTokensDTO.Token && 
            b.UserId == refreshTokensDTO.UserId && 
            b.Fingerprint == refreshTokensDTO.Fingerprint;
            return predicate;
        }
        #endregion

        public TokenService(IGenericRepository repository, IMapper mapper, IAuthOptions authOptions)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _authOptions = authOptions ?? throw new ArgumentNullException(nameof(authOptions));
            _envirementType = EnvirementTypes.Development;
        }

        #region Get Methods
        public async Task<int> GetTokenSettingsAsync(EnvirementTypes type)
        {
            var predicate = GetTokenSettingsPredicate(type);
            var tokenSetting = await _repository.GetAsync(predicate, x => x.EnvironmentType) ?? throw new NotFoundInDatabaseException();
            return tokenSetting.LifeTime;
        }
        #endregion

        #region Add methods
        public async Task<TokensDTO> AddRefreshTokenAsync(ClaimsIdentity identity, string fingerprint)
        {
            if(identity == null) throw new ArgumentNullException(nameof(identity));

            var tokenSettingsDTO = await TokenSettingsDTOCreateAsync(_envirementType, identity);
            var tokens = _authOptions.GetJWTTokens(tokenSettingsDTO);
            var userId = identity.GetUserId<long>();
            var lifeTime = await GetTokenSettingsAsync(_envirementType);

            var refreshToken = new RefreshTokens()
            {
                Token = tokens.RefreshToken,
                UserId = userId,
                Fingerprint = fingerprint,
                ExpiresAt = DateTime.UtcNow.AddDays(lifeTime)
        };

            await _repository.AddAsync(refreshToken);

            if (refreshToken.Id == IdConst.NotFound) throw new FailedAddToDatabaseException();
            return tokens;
        }
        #endregion

        public async Task<TokensDTO?> RefreshTokenProccessingAsync(string fingerprint, TokensDTO tokensDTO)
        {
            if (fingerprint == null) throw new ArgumentNullException(nameof(fingerprint));
            if (tokensDTO == null) throw new ArgumentNullException(nameof(tokensDTO));

            // Get user data from access token
            var principal = _authOptions.GetPrincipalFromExpiredToken(tokensDTO.AccessToken);
            if (principal.Identity == null)
                return null;
            var userId = principal.Identity.GetUserId<long>();


            // Get refresh token from DB if data matches
            var refreshTokenDTO = new RefreshTokensDTO()
            {
                Token = tokensDTO.RefreshToken,
                Fingerprint = fingerprint,
                UserId = userId
            };
            var predicate = GetRefreshTokenPredicate(refreshTokenDTO);
            var refreshToken = await _repository.GetAsync(predicate);

            if (refreshToken == null)
                return null;

            // If token has expired delete token
            if (refreshToken.ExpiresAt < DateTime.UtcNow)
            {
                await DeleteRefreshTokenAsync(refreshToken);
                return null;
            }

            var tokenSettingsDTO = await TokenSettingsDTOCreateAsync(_envirementType, 
                principal.Identity as ClaimsIdentity);
            var tokens = _authOptions.GetJWTTokens(tokenSettingsDTO);
            if(await UpdateRefreshTokenAsync(refreshTokenDTO, tokens.RefreshToken))
                return tokens;
            return null;
        }

        #region Private Methods
        private async Task<bool> UpdateRefreshTokenAsync(RefreshTokensDTO oldRefreshTokenDTO, string newRefreshToken)
        {
            if (oldRefreshTokenDTO == null) throw new ArgumentNullException(nameof(oldRefreshTokenDTO));
            if (newRefreshToken == null) throw new ArgumentNullException(nameof(newRefreshToken));

            var predicate = GetRefreshTokenPredicate(oldRefreshTokenDTO);
            var refreshToken = await _repository.GetAsync(predicate);
            refreshToken.Token = newRefreshToken;
            var lifeTime = await GetTokenSettingsAsync(_envirementType);
            refreshToken.ExpiresAt = DateTime.UtcNow.AddDays(lifeTime);
            await _repository.UpdateAsync(refreshToken);
            return true;
        }

        private async Task<bool> DeleteRefreshTokenAsync(RefreshTokens refreshToken) 
        {
            if (refreshToken == null) throw new ArgumentNullException(nameof(refreshToken));

            await _repository.DeleteAsync(refreshToken);
            return true;
        }

        private async Task<TokenSettingsDTO> TokenSettingsDTOCreateAsync(EnvirementTypes type, ClaimsIdentity identity)
        {
            if(identity == null) throw new ArgumentNullException(nameof(identity));

            var lifeTime = await GetTokenSettingsAsync(type);
            return new TokenSettingsDTO { Identity = identity, LifeTime = lifeTime };
        }
        #endregion
    }
}
