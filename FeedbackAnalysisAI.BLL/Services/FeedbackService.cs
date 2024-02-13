using AutoMapper;
using FeedbackAnalysisAI.BLL.Exceptions;
using FeedbackAnalysisAI.Contracts.Consts;
using FeedbackAnalysisAI.Contracts.DTO;
using FeedbackAnalysisAI.Contracts.Services;
using FeedbackAnalysisAI.DAL.Entities;
using FeedbackAnalysisAI.DAL.Interfaces;
using System.Linq.Expressions;

namespace FeedbackAnalysisAI.BLL.Services
{
    public class FeedbackService : IFeedbackService
    {
        #region Repository
        private readonly IGenericRepository _repository;
        #endregion

        #region Services
        private readonly IMapper _mapper;
        private readonly IAzureAiServiceProvider _azureAiServiceProvider;
        #endregion

        #region Predicates
        private Expression<Func<Feedback, bool>> GetFeedbackPredicate(long feedbackId)
        {
            Expression<Func<Feedback, bool>> predicate = b => b.Id == feedbackId;
            return predicate;
        }

        private Expression<Func<Sentiment, bool>> GetSentimentByNamePredicate(string sentimentName)
        {
            Expression<Func<Sentiment, bool>> predicate = a => a.Name == sentimentName;
            return predicate;
        }
        #endregion

        public FeedbackService(IGenericRepository repository, IMapper mapper, IAzureAiServiceProvider azureAiServiceProvider)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _azureAiServiceProvider = azureAiServiceProvider ?? throw new ArgumentNullException(nameof(azureAiServiceProvider));
        }
        public async Task<FeedbackDTO> GetFeedbackAsync(long feedbackId)
        {
            if (feedbackId <= IdConst.NotFound) throw new NotFoundInDatabaseException();
            var predicate = GetFeedbackPredicate(feedbackId);
            var feedback = await _repository.GetAsync(predicate, x => x.Sentiment) ?? throw new NotFoundInDatabaseException();
            var feedbackDTO = _mapper.Map<FeedbackDTO>(feedback);
            feedbackDTO.SentimentName = feedback.Sentiment.Name;
            return feedbackDTO;
        }

        public async Task<long> AddFeedbackAsync(FeedbackDTO feedbackDTO)
        {
            if (feedbackDTO == null) throw new ArgumentNullException();

            var feedback = _mapper.Map<Feedback>(feedbackDTO);
            feedback.Sentiment = await FeedbackProccessingSentimentAiAsync(feedbackDTO.Text);

            await _repository.AddAsync(feedback);
            if (feedback.Id <= IdConst.NotFound) throw new FailedAddToDatabaseException();

            return feedback.Id;
        }

        public async Task<List<FeedbackDTO>> GetFeedbacksListAsync()
        {
            var feedbacksList = await _repository.ListAsync<Feedback>(x => x.Id>0,x => x.Sentiment) ?? throw new NotFoundInDatabaseException();

            var feedbacksDTO = _mapper.Map<List<FeedbackDTO>>(feedbacksList);
            return feedbacksDTO;
        }

        private async Task<Sentiment> FeedbackProccessingSentimentAiAsync(string feedbackText)
        {
            var azureSentiment = await _azureAiServiceProvider.AnalyzeSentimentAsync(feedbackText);
            var predicate = GetSentimentByNamePredicate(azureSentiment.ToString());
            var sentiment = await _repository.GetAsync(predicate) ?? throw new NotFoundInDatabaseException();
            return sentiment;
        }
    }
}
