using AutoMapper;
using FeedbackAnalysisAI.Contracts.DTO;
using FeedbackAnalysisAI.DAL.Entities;

namespace FeedbackAnalysisAI.BLL.MappingProfiles
{
    public class FeedbackProfileBLL : Profile
    {
        public FeedbackProfileBLL()
        {
            #region To DTO
            CreateMap<Feedback, FeedbackDTO>()
                .ForMember(dest => dest.SentimentName, opt => opt.MapFrom(src => src.Sentiment.Name));
            #endregion

            #region from DTO
            CreateMap<FeedbackDTO, Feedback>();
            #endregion
        }
    }
}
