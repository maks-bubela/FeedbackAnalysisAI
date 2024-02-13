using Autofac;
using FeedbackAnalysisAI.BLL.Cryptography;
using FeedbackAnalysisAI.BLL.Services;
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

            base.Load(builder);
        }
    }
}
