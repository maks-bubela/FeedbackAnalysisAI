using AutoMapper;
using FeedbackAnalysisAI.Contracts.DTO;
using FeedbackAnalysisAI.DAL.Entities;

namespace FeedbackAnalysisAI.BLL.MappingProfiles
{
    public class RefreshTokenProfileBLL : Profile
    {
        public RefreshTokenProfileBLL()
        {
            #region To DTO
            CreateMap<RefreshTokens, RefreshTokensDTO>();
            #endregion

            #region from DTO
            CreateMap<RefreshTokensDTO, RefreshTokens>();
            #endregion
        }
    }
}
