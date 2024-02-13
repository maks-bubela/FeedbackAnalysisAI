using FeedbackAnalysisAI.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FeedbackAnalysisAI.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId).IsRequired();

            builder.HasMany(u => u.Feedbacks)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId).IsRequired();
        }
    }
}
