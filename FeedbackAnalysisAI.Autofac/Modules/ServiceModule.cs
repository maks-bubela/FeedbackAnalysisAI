using Autofac;
using FeedbackAnalysisAI.BLL.Cryptography;
using FeedbackAnalysisAI.BLL.Services;
using FeedbackAnalysisAI.BLL.JwtConfig.Provider;
using FeedbackAnalysisAI.Contracts.Services;


namespace FeedbackAnalysisAI.Autofac.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<FeedbackService>().As<IFeedbackService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<TokenService>().As<ITokenService>();
            builder.RegisterType<PasswordProcessing>().As<IPasswordProcessing>();
            builder.RegisterType<AuthOptions>().As<IAuthOptions>();

            base.Load(builder);
        }
    }
}
