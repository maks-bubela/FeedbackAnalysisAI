using Autofac;
using System.Text.Json.Serialization;
using FeedbackAnalysisAI.API.ExtensionMethods;
using FeedbackAnalysisAI.API.Filters;
using FeedbackAnalysisAI.DAL.Context;

namespace FeedbackAnalysisAI.API.AppStart
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader());
            });
            services.AddSwagger();
            services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            services.AddJwtToken(Configuration);
            services.AddOptions();
            services.AddMvc(options => options.Filters.Add(new ExceptionFilter()));
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            DIConfig.Configure(builder);

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<FeedbackAnalysisAIContext>();
                context.Database.EnsureCreated();
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCustomSwagger();
            app.UseCors("CorsPolicy");
            app.UseCorsMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
