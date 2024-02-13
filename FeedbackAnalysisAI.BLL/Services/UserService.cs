using AutoMapper;
using FeedbackAnalysisAI.BLL.Exceptions;
using FeedbackAnalysisAI.DAL.Entities;
using System.Linq.Expressions;
using FeedbackAnalysisAI.DAL.Interfaces;
using FeedbackAnalysisAI.Contracts.Consts;
using FeedbackAnalysisAI.Contracts.Services;
using FeedbackAnalysisAI.Contracts.DTO;

namespace FeedbackAnalysisAI.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository _repository;

        public UserService(IMapper mapper, IGenericRepository repository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        #region Predicates
        private Expression<Func<User, bool>> GetActiveUserByIdPredicate(long userId)
        {
            Expression<Func<User, bool>> predicate = x => x.Id == userId;
            return predicate;
        }
        private Expression<Func<User, bool>> GetActiveUserByUsernamePredicate(string username)
        {
            Expression<Func<User, bool>> predicate = x => x.Username == username;
            return predicate;
        }

        #endregion

        #region Read Methods
        public async Task<UserDTO> GetUserByIdAsync(long id)
        {
            if (id <= IdConst.NotFound) throw new EntityArgumentNullException(nameof(id));

            var predicate = GetActiveUserByIdPredicate(id);
            var user = await _repository.GetAsync(predicate, x => x.Role);
            if (user == null) throw new ArgumentNullException(nameof(user));

            var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }

        public async Task<UserDTO> GetUserByUsernameAsync(string username)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));

            var predicate = GetActiveUserByUsernamePredicate(username);
            var user = await _repository.GetAsync(predicate, x => x.Role);
            if (user == null) throw new NullReferenceException(nameof(user));

            var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }

        public async Task<bool> UserExistsAsync(long id)
        {
            var predicate = GetActiveUserByIdPredicate(id);
            var exists = await _repository.IsExistAsync(predicate);
            return exists;
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            var predicate = GetActiveUserByUsernamePredicate(username);
            var exists = await _repository.IsExistAsync(predicate);
            return exists;
        }
        #endregion
    }
}
