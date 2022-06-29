using Microsoft.Extensions.DependencyInjection;
using Repositories.Interfaces;

namespace Repositories.Extensions
{
    public static class RegisterRepositoriesExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<ITextRepository, TextRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();

            return services;
        }
    }
}
