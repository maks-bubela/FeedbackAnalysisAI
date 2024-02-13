using Autofac;
using AutoMapper;
using FeedbackAnalysisAI.API.Interfaces;
using FeedbackAnalysisAI.API.JwtConfig.Provider;
using FeedbackAnalysisAI.API.MappingProfiles;
using FeedbackAnalysisAI.Autofac.Modules;
using FeedbackAnalysisAI.BLL.MappingProfiles;

namespace FeedbackAnalysisAI.API.AppStart
{
    public class DIConfig
    {
        public static ContainerBuilder Configure(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule<DALModule>();
            containerBuilder.RegisterModule<AzureModule>();
            containerBuilder.RegisterModule<ServiceModule>();
            containerBuilder.RegisterType<AuthOptions>().As<IAuthOptions>();
            containerBuilder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserProfileBLL());
                cfg.AddProfile(new UserProfile());
                cfg.AddProfile(new FeedbackProfile());
                cfg.AddProfile(new FeedbackProfileBLL());
            }).CreateMapper()).As<IMapper>().InstancePerLifetimeScope();
            return containerBuilder;
        }

    }
}
