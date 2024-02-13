using AutoMapper;
using FeedbackAnalysisAI.Contracts.DTO;
using FeedbackAnalysisAI.Contracts.Models;

namespace FeedbackAnalysisAI.API.MappingProfiles
{
    public class FeedbackProfile : Profile
    {
        public FeedbackProfile()
        {
            #region To Model
            CreateMap<FeedbackDTO, FeedbackCreateModel>();
            CreateMap<FeedbackDTO, FeedbackAndSentiemntModel>();
            #endregion

            #region To DTO
            CreateMap<FeedbackCreateModel, FeedbackDTO>();
            CreateMap<FeedbackAndSentiemntModel, FeedbackDTO>();
            #endregion
        }
    }
}
