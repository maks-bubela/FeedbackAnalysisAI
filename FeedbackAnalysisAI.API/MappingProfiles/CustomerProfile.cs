using AutoMapper;
using FeedbackAnalysisAI.Contracts.DTO;
using FeedbackAnalysisAI.Contracts.Models;

namespace FeedbackAnalysisAI.API.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            #region To Model
            CreateMap<UserDTO, UserLoginModel>();
            CreateMap<UserDTO, UserRegistrationModel>();
            #endregion

            #region To DTO
            CreateMap<UserLoginModel, UserDTO>();
            CreateMap<UserRegistrationModel, UserDTO>();
            #endregion
        }
    }
}
