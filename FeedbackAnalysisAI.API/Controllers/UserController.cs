using AutoMapper;
using FeedbackAnalysisAI.Contracts.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackAnalysisAI.API.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets authenticated user info (role, fullname and username).
        /// </summary>
        /// <returns></returns>
        /// <response code="200"> Info was found </response>>
        /// <response code="500"> Something is wrong on server </response>>
        [HttpGet, Route("info")]
        [Authorize]
        public async Task<IActionResult> GetInfoAsync()
        {
            var userId = User.Identity.GetUserId<long>();
            var userDTO = await _userService.GetUserByIdAsync(userId);

            if (userDTO != null)
            {
                var response = new
                {
                    Username = userDTO.Username,
                    Firstname = userDTO.Firstname,
                    Lastname = userDTO.Lastname,
                    Email = userDTO.Email,
                    Role = userDTO.RoleName
                };
                return Json(response);
            }
            return StatusCode(500);
        }
    }
}
