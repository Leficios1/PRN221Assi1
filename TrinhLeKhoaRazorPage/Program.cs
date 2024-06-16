using DataAccessObject.Model;
using Microsoft.EntityFrameworkCore;
using Services.DTO.Response;
using System.Configuration;

namespace TrinhLeKhoaRazorPage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            Extensions.ServiceCollectionExtensions.Register(builder.Services);

            //Connect With DB
            builder.Services.AddDbContext<FunewsManagementDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("FUNewsManagementDB"));
            });
            builder.Services.Configure<AdminAccount>(builder.Configuration.GetSection("AdminAccount"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            //app.MapFallbackToPage("/Admin/Index");

            app.Run();
        }
    }
}