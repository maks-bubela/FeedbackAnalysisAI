using AutoMapper;
using FeedbackAnalysisAI.Contracts.DTO;
using FeedbackAnalysisAI.Contracts.Models;

namespace FeedbackAnalysisAI.API.MappingProfiles
{
    public class TokensProfile : Profile
    {
        public TokensProfile()
        {
            #region To Model
            CreateMap<TokensDTO, TokensModel>();
            #endregion

            #region To DTO
            CreateMap<TokensModel, TokensDTO>();
            #endregion
        }
    }
}
