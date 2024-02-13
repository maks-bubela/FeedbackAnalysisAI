using FeedbackAnalysisAI.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FeedbackAnalysisAI.DAL.Configurations
{
    internal class SentimentConfiguration : IEntityTypeConfiguration<Sentiment>
    {
        public void Configure(EntityTypeBuilder<Sentiment> builder)
        {
            builder.HasMany(u => u.Feedbacks)
                .WithOne(r => r.Sentiment)
                .HasForeignKey(u => u.SentimentId);
        }
    }
}
