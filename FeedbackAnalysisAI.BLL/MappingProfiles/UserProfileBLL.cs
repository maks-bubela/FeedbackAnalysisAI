using AutoMapper;
using FeedbackAnalysisAI.Contracts.DTO;
using FeedbackAnalysisAI.DAL.Entities;

namespace FeedbackAnalysisAI.BLL.MappingProfiles
{
    public class UserProfileBLL : Profile
    {
        public UserProfileBLL()
        {
            #region To DTO
            CreateMap<User, UserDTO>();
            #endregion

            #region from DTO
            CreateMap<UserDTO, User>();
            #endregion
        }
    }
}
