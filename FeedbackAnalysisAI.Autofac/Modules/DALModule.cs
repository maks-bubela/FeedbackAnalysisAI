using Autofac;
using FeedbackAnalysisAI.Configuration;
using FeedbackAnalysisAI.DAL.Context;
using FeedbackAnalysisAI.DAL.Interfaces;
using FeedbackAnalysisAI.DAL.Repository;

namespace FeedbackAnalysisAI.Autofac.Modules
{
    public class DALModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var connectionString = FeedbackAnalysisAIConfiguration.CreateFromConfigurations().SqlConnectionString;

            builder.Register(ctx => new FeedbackAnalysisAIContext(connectionString)).AsSelf();
            builder.RegisterType<GenericRepository>().As<IGenericRepository>();

            base.Load(builder);
        }
    }
}
