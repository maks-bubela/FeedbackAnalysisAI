
using FeedbackAnalysisAI.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FeedbackAnalysisAI.DAL.Configurations
{
    public class RefreshTokensConfiguration : IEntityTypeConfiguration<RefreshTokens>
    {
        public void Configure(EntityTypeBuilder<RefreshTokens> builder)
        {
            builder.HasOne(u => u.User)
                .WithMany(r => r.RefreshTokens)
                .HasForeignKey(u => u.UserId).IsRequired();
        }
    }
}
