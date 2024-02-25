using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using FeedbackAnalysisAI.Contracts.Models;
using System.Security.Claims;
using FeedbackAnalysisAI.Contracts.Consts;
using FeedbackAnalysisAI.Contracts.Services;
using FeedbackAnalysisAI.Contracts.DTO;

namespace FeedbackAnalysisAI.API.Controllers
{
    [Route("api/authentication")]
    public class AuthenticationController : Controller
    {
        #region Constants
        private const string InvalidUserData = "Invalid username or password.";
        private const string InvalidModel = "Invalid input model.";
        #endregion

        #region Services
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        #endregion


        public AuthenticationController(ITokenService tokenService, IAccountService accountService,
            IMapper mapper, IUserService userService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(IAccountService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
           //_authOptions = authOptions ?? throw new ArgumentNullException(nameof(IAuthOptions));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(ITokenService));
            _userService = userService ?? throw new ArgumentNullException(nameof(IUserService));

    }

        /// <summary>
        /// Authenticates a customer and returns generated token if authentication is successful
        /// </summary>
        /// <param name="customer">User and login information to authenticate</param>
        /// <returns></returns>
        /// <response code="200"> User is authenticated </response>
        /// <response code="409"> Authentication is failed </response>
        /// <response code="204"> Not founded environment type </response>
        /// <response code="400">Model not valid</response>    
        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> TokenAsync([FromBody] UserLoginModel customer)
        {
            if (ModelState.IsValid)
            {
                var verified = await _accountService.VerifyCredentialsAsync(customer.Username, customer.Password);
                if (!verified)
                    return Conflict(new { errorText = InvalidUserData });
                var identity = await GetIdentityAsync(customer);
                var fingerprint = HttpContext.Connection.RemoteIpAddress.ToString();

                var tokensDTO = await _tokenService.AddRefreshTokenAsync(identity, fingerprint);
                var tokens = _mapper.Map<TokensModel>(tokensDTO);
                if (tokens == null)
                    return StatusCode(500);
                return Ok(tokens);
            }
            return BadRequest(new { errorText = InvalidUserData });
        }

        /// <summary>
        /// Refresh  customer token if old token not expired
        /// </summary>
        /// <param name="tokenModel">Old refresh and access token</param>
        /// <returns></returns>
        /// <response code="200"> Token is refreshed </response>
        /// <response code="401"> Refresh token not valid </response>
        /// <response code="204"> Not founded environment type </response>
        /// <response code="400">Model not valid</response>    
        [HttpPost]
        [Route("refresh/token")]
        public async Task<IActionResult> RefreshTokenAsync(TokensModel tokenModel)
        {
            if (ModelState.IsValid)
            {
                string fingerPrint = HttpContext.Connection.RemoteIpAddress.ToString();
                var tokensDTO = _mapper.Map<TokensDTO>(tokenModel);
                var tokens = await _tokenService.RefreshTokenProccessingAsync(fingerPrint, tokensDTO);
                if (tokens == null)
                    return Unauthorized();
                return Ok(tokens);
            }
            return BadRequest();
        }

        /// <summary>
        /// Registration new customer and add him into database
        /// </summary>
        /// <param name="customerRegist">User and login information for registration</param>
        /// <param name="roleName">User role name</param>
        /// <returns></returns>
        /// <response code="201"> User succesfully registrated </response>
        /// <response code="409"> Registration is failed </response>
        /// <response code="400"> Model not valid </response>    
        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> RegistrationAsync(
            [FromBody] UserRegistrationModel customerRegist, [FromQuery] string? roleName)
        {
            if (ModelState.IsValid && customerRegist != null)
            {
                var userDTO = _mapper.Map<UserDTO>(customerRegist);
                userDTO.RoleName = roleName;

                var customerId = await _accountService.RegisterUserAsync(userDTO);
                if (customerId != 0)
                {
                    var response = new
                    {
                        Username = userDTO.Username
                    };
                    return Created(roleName, response);
                }
                return Conflict();
            }
            return BadRequest(new { errorText = InvalidModel });
        }

        /// <summary>
        /// Private method that return claims identity about user
        /// </summary>
        /// <param name="customerRegist">User and login information for registration</param>
        /// <param name="roleName">User role name</param>
        /// <returns></returns>
        private async Task<ClaimsIdentity> GetIdentityAsync(UserLoginModel customerModel)
        {
            var verified = await _accountService.VerifyCredentialsAsync(customerModel.Username, customerModel.Password);
            if (verified)
            {
                var userDTO = await _userService.GetUserByUsernameAsync(customerModel.Username);
                if (userDTO != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, userDTO.Username),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, userDTO.RoleName),
                        new Claim(ClaimTypes.NameIdentifier, userDTO.Id.ToString())
                    };
                    ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, TokenConsts.TokenKey, ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                    return claimsIdentity;
                }
            }
            return null;
        }
    }
}
