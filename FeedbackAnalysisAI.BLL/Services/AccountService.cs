using AutoMapper;
using FeedbackAnalysisAI.BLL.Exceptions;
using FeedbackAnalysisAI.DAL.Entities;
using FeedbackAnalysisAI.DAL.Interfaces;
using System.Linq.Expressions;
using FeedbackAnalysisAI.Contracts.Services;
using FeedbackAnalysisAI.Contracts.DTO;
using FeedbackAnalysisAI.Contracts.Enums;


namespace FeedbackAnalysisAI.BLL.Services
{
    public class AccountService : IAccountService
    {
        #region Services
        private readonly IMapper _mapper;
        private readonly IUserService _customerService;
        private readonly IGenericRepository _repository;
        private readonly IPasswordProcessing _passProcess;
        #endregion

        #region Predicates
        private Expression<Func<Role, bool>> GetRoleByNamePredicate(string customerRoleName)
        {
            Expression<Func<Role, bool>> predicate = a => a.Name == customerRoleName;
            return predicate;
        }
        private Expression<Func<User, bool>> GetActiveUserPredicate(string customerUsername)
        {
            Expression<Func<User, bool>> predicate = a => a.Username == customerUsername && !a.IsDelete;
            return predicate;
        }
        #endregion

        public AccountService(IMapper mapper, IPasswordProcessing passProcess, 
            IUserService customerService, IGenericRepository repository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _passProcess = passProcess ?? throw new ArgumentNullException(nameof(passProcess));
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        #region Create methods
        public async Task<long> RegisterUserAsync(UserDTO userDTO)
        {
            if (userDTO == null) throw new ArgumentException(nameof(userDTO));
            if (await _customerService.UserExistsAsync(userDTO.Username)) throw new DataExistsInDatabaseException();
            var salt = _passProcess.GenerateSalt();

            var predicate = GetRoleByNamePredicate(userDTO.RoleName);
            var role = await _repository.GetAsync(predicate);
            if (role == null)
            {
                role = await _repository.
                    GetAsync<Role>((long)Roles.User) ?? throw new NotFoundInDatabaseException();
            }

            var user = _mapper.Map<User>(userDTO);
            user.Password = _passProcess.GetHashCode(userDTO.Password, salt);
            user.Salt = salt;
            user.RoleId = role.Id;
            user.IsDelete = false;
            await _repository.AddAsync(user);

            if (!await _customerService.UserExistsAsync(user.Id)) throw new FailedAddToDatabaseException();
            return user.Id;
        }
        #endregion

        #region Read methods
        public async Task<bool> VerifyCredentialsAsync(string username, string password)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));
            if (password == null) throw new ArgumentNullException(nameof(password));

            var predicate = GetActiveUserPredicate(username);
            var user = await _repository.GetAsync(predicate);
            if (user != null)
            {
                string pass = _passProcess.GetHashCode(password, user.Salt);
                if (user.Password == pass)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
