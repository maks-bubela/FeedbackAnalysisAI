using FeedbackAnalysisAI.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackAnalysisAI.DAL.Configurations
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.HasOne(u => u.User)
                .WithMany(r => r.Feedbacks)
                .HasForeignKey(u => u.UserId);

            builder.HasOne(u => u.Sentiment)
                .WithMany(r => r.Feedbacks)
                .HasForeignKey(u => u.SentimentId);
        }
    }
}
