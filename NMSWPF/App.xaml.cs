using AutoMapper;
using DataAccessObject.Model;
using DataAccessObject.Repository;
using DataAccessObject.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.DTO.Response;
using Services.Mapper;
using Services.Services;
using Services.Services.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TrinhLekhoaWPF;

namespace NMSWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IConfiguration Configuration { get; }
        public AdminAccount adminAccount { get; set; }
        public IServiceProvider ServiceProvider { get; set; }

        private void ConfigureServices(IServiceCollection services)
        {
            //Register Services here
            services.AddSingleton<ISystemAccountServices, SystemAccountServices>();
            //Register Repository here
            services.AddSingleton(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddSingleton<ISystemAccountRepository, SystemAccountRepository>();
            services.AddSingleton<INewsArticleRepository, NewsArticleRepository>();

            //Register DBcontext
            services.AddDbContext<FunewsManagementDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString(Configuration.GetConnectionString("FUNewsManagementDB"))));

            //Register AutoMapper
            //services.AddAutoMapper(typeof(App).Assembly);
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingEntities());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
            //Read Infor of Admin
            adminAccount = Configuration.GetSection("AdminAccount").Get<AdminAccount>();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var loginWindow = new Login(adminAccount);
            loginWindow.Show();
        }
    }
}
