using DataAccessObject.Repository;
using DataAccessObject.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;
using Services.DTO.Response;
using Services.Services;
using Services.Services.Interface;
using System.Configuration;

namespace TrinhLeKhoaRazorPage.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection Register(this IServiceCollection services)
        {
            services.AddRazorPages();

            //Register AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Register Session
            services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromMinutes(30);
                option.Cookie.HttpOnly = true;
                option.Cookie.IsEssential = true;
            });

            //Reigster Repository 
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ISystemAccountRepository, SystemAccountRepository>();
            services.AddScoped<INewsArticleRepository, NewsArticleRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITagRepository, TagRepository>();

            //Register Services
            services.AddScoped<ISystemAccountServices, SystemAccountServices>();
            services.AddScoped<INewsArticleServices, NewsArticleServices>();
            services.AddScoped<ICategoryServices, CategoryServices>();
            services.AddScoped<ITagServices, TagServices>();

            return services;
        }
    }
}
