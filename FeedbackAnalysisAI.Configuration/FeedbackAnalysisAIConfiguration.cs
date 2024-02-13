using Microsoft.Extensions.Configuration;

namespace FeedbackAnalysisAI.Configuration
{
    public class FeedbackAnalysisAIConfiguration
    {
        public const string SqlConnectionStringSectionName = "ConnectionStrings:FeedbackAnalysisAI_DB";
        public const string AzureAiEndpointSectionName = "AzureServiceSettings:AzureAiEndpoint";
        public const string AzureAiApiKeySectionName = "AzureServiceSettings:AzureAiApiKey";

        public string SqlConnectionString { get; private set; }
        public Uri AzureAiEndpoint { get; private set; }
        public string AzureAiApiKey { get; private set; }

        public FeedbackAnalysisAIConfiguration(string sqlConnectionString, Uri azureAiEndpoint, string azureAiApiKey)
        {
            if (string.IsNullOrWhiteSpace(nameof(sqlConnectionString)))
            {
                throw new ArgumentException(nameof(sqlConnectionString));
            }
            if (string.IsNullOrWhiteSpace(nameof(azureAiEndpoint)))
            {
                throw new ArgumentException(nameof(azureAiEndpoint));
            }
            if (string.IsNullOrWhiteSpace(nameof(azureAiApiKey)))
            {
                throw new ArgumentException(nameof(azureAiApiKey));
            }

            SqlConnectionString = sqlConnectionString;
            AzureAiEndpoint = azureAiEndpoint;
            AzureAiApiKey = azureAiApiKey;
        }

        public static FeedbackAnalysisAIConfiguration CreateFromConfigurations()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            IConfigurationRoot root = configurationBuilder.Build();

            return new FeedbackAnalysisAIConfiguration(
                sqlConnectionString: root.GetSection(SqlConnectionStringSectionName).Value,
                azureAiEndpoint:new Uri( root.GetSection(AzureAiEndpointSectionName).Value),
                azureAiApiKey: root.GetSection(AzureAiApiKeySectionName).Value);
        }
    }
}
