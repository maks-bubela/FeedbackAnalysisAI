using FeedbackAnalysisAI.BLL.Exceptions;
using FeedbackAnalysisAI.Contracts.Enums;
using FeedbackAnalysisAI.Contracts.Services;
using FeedbackAnalysisAI.DAL.Entities;
using FeedbackAnalysisAI.DAL.Interfaces;
using System.Linq.Expressions;


namespace FeedbackAnalysisAI.BLL.Services
{
    public class TokenService : ITokenService
    {
        #region Repository
        private readonly IGenericRepository _repository;
        #endregion

        #region Predicates
        private Expression<Func<BearerTokenSetting, bool>> GetTokenSettingsPredicate(EnvirementTypes type)
        {
            Expression<Func<BearerTokenSetting, bool>> predicate = b => b.EnvironmentType.Id == ((long)type);
            return predicate;
        }
        #endregion

        public TokenService(IGenericRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<int> GetTokenSettingsAsync(EnvirementTypes type)
        {
            var predicate = GetTokenSettingsPredicate(type);
            var tokenSetting = await _repository.GetAsync(predicate, x => x.EnvironmentType) ?? throw new NotFoundInDatabaseException();
            return tokenSetting.LifeTime;
        }
    }
}
