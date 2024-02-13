using FeedbackAnalysisAI.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FeedbackAnalysisAI.DAL.Seeder
{
    public static class DatabaseSeeder
    {
        public static void SeedDataBase(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<EnvironmentType>().HasData(
                    new EnvironmentType { Id = 1, Name = "staging" },
                    new EnvironmentType { Id = 2, Name = "development" },
                    new EnvironmentType { Id = 3, Name = "testing" },
                    new EnvironmentType { Id = 4, Name = "production" }
                );

            modelBuilder.Entity<BearerTokenSetting>().HasData(
                    new BearerTokenSetting { Id = 1, EnvironmentTypeId = 1, LifeTime = 30 },
                    new BearerTokenSetting { Id = 2, EnvironmentTypeId = 2, LifeTime = 30 },
                    new BearerTokenSetting { Id = 3, EnvironmentTypeId = 3, LifeTime = 1 },
                    new BearerTokenSetting { Id = 4, EnvironmentTypeId = 4, LifeTime = 7 }
                );

            modelBuilder.Entity<Role>().HasData(
                    new Role() { Id = 1, Name = "admin" },
                    new Role() { Id = 2, Name = "staff" },
                    new Role() { Id = 3, Name = "user" }
                );

            modelBuilder.Entity<Sentiment>().HasData(
                    new Sentiment() { Id = 1, Name = "Positive" },
                    new Sentiment() { Id = 2, Name = "Negative" },
                    new Sentiment() { Id = 3, Name = "Neutral" },
                    new Sentiment() { Id = 4, Name = "Mixed" }
    );
        }
    }
}
