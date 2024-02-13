using Microsoft.EntityFrameworkCore;
using FeedbackAnalysisAI.DAL.Entities;
using FeedbackAnalysisAI.DAL.Seeder;
using System.Reflection;

namespace FeedbackAnalysisAI.DAL.Context
{
    public class FeedbackAnalysisAIContext : DbContext
    {
        public FeedbackAnalysisAIContext(string connectionString) : base(GetOptions(connectionString)) { }
        public FeedbackAnalysisAIContext(DbContextOptions<FeedbackAnalysisAIContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Sentiment> Sentiments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<BearerTokenSetting> BearerTokenSettings { get; set; }
        public DbSet<EnvironmentType> EnvironmentTypes { get; set; }


        private static DbContextOptions<FeedbackAnalysisAIContext> GetOptions(string connectionString)
        {
            return new DbContextOptionsBuilder<FeedbackAnalysisAIContext>()
                .UseNpgsql(connectionString)  
                .Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            DatabaseSeeder.SeedDataBase(modelBuilder);
            base.OnModelCreating(modelBuilder);

        }
    }
}
