using AutoMapper;
using FeedbackAnalysisAI.BLL.Services;
using FeedbackAnalysisAI.Contracts.DTO;
using FeedbackAnalysisAI.Contracts.Models;
using FeedbackAnalysisAI.Contracts.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackAnalysisAI.API.Controllers
{
    [Route("api/feedback")]
    [ApiController]
    public class FeedbackController : Controller
    {
        private readonly IAzureAiServiceProvider _aiServiceProvider;
        private readonly IFeedbackService _feedbackService;
        private readonly IMapper _mapper;

        public FeedbackController(IMapper mapper, IAzureAiServiceProvider aiServiceProvider, IFeedbackService feedbackService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _aiServiceProvider = aiServiceProvider ?? throw new ArgumentNullException(nameof(_aiServiceProvider));
            _feedbackService = feedbackService ?? throw new ArgumentNullException(nameof(feedbackService));
        }

        /// <summary>
        /// Add user feedback.
        /// </summary>
        /// <returns></returns>
        /// <param name="feedbackCreateModel">Feedback text</param>
        /// <response code="201"> Feedback was succesfully created </response>>
        /// <response code="401"> Invalid model </response>>
        /// <response code="500"> Something is wrong on server </response>>
        [HttpPost, Route("add")]
        [Authorize]
        public async Task<IActionResult> AddFeedback(FeedbackCreateModel feedbackCreateModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId<long>();
                var feedbackDTO = _mapper.Map<FeedbackDTO>(feedbackCreateModel);
                feedbackDTO.UserId = userId;
                var feedbackId = await _feedbackService.AddFeedbackAsync(feedbackDTO);
                var response = new { FeedbackId = feedbackId };
                return Ok(response);
            }
            return BadRequest();
        }

        /// <summary>
        /// Gets feedback info.
        /// </summary>
        /// <returns></returns>
        /// <response code="200"> Info was found </response>>
        /// <response code="404"> Info not found </response>>
        /// <response code="500"> Something is wrong on server </response>>
        [HttpGet, Route("info")]
        [Authorize]
        public async Task<IActionResult> GetFeedbackInfoAsync(long feedbackId)
        {
            var feedbackDTO = await _feedbackService.GetFeedbackAsync(feedbackId);
            if(feedbackDTO == null)
                return NotFound();
            var response = new
            {
                UserId = feedbackDTO.UserId,
                Text = feedbackDTO.Text,
                Sentiment = feedbackDTO.SentimentName,
            };
            return Json(response);
        }

        /// <summary>
        /// Gets authenticated feedbacks list info.
        /// </summary>
        /// <returns></returns>
        /// <response code="200"> Info was found </response>>
        /// <response code="500"> Something is wrong on server </response>>
        [Authorize, HttpGet]
        [Route("list/info")]
        public async Task<IActionResult> GetFeedbacksListAsync()
        {
            var feedbacksListDTO = await _feedbackService.GetFeedbacksListAsync();
            if (feedbacksListDTO == null) return StatusCode(500);

            var feedbacksListInfo = _mapper.Map<List<FeedbackAndSentiemntModel>>(feedbacksListDTO);
            return Ok(new { feedbacksListInfo });
        }
    }
}
