using Autofac;
using FeedbackAnalysisAI.AzureService.AiService.Provider;
using FeedbackAnalysisAI.Configuration;
using FeedbackAnalysisAI.Contracts.Services;

namespace FeedbackAnalysisAI.Autofac.Modules
{
    public class AzureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var config = FeedbackAnalysisAIConfiguration.CreateFromConfigurations();
            var endpoint = config.AzureAiEndpoint;
            var api = config.AzureAiApiKey;
            var azureApi = new Azure.AzureKeyCredential(api);
            builder.Register(prov => new AzureAiServiceProvider(endpoint, azureApi)).AsSelf().As<IAzureAiServiceProvider>();
            base.Load(builder);
        }
    }
}
