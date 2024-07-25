using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Interface;
using Repositories;
using Services.Interface;
using Services;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IConfiguration Configuration { get; }
        public IServiceProvider ServiceProvider { get; set; }

        public App()
        {
            var builder =new ConfigurationBuilder()
                   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                   .AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            var services = new ServiceCollection();
            ConfigServices(services);
            ServiceProvider = services.BuildServiceProvider();
        }

        private void ConfigServices(IServiceCollection services)
        {

            //Register Dbcontext
            services.AddDbContext<FuminiHotelManagementContext>(options =>
                     options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));


            //Register Services here
            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddSingleton<IBookingHistoryService, BookingHistoryService>();
            services.AddSingleton<IRoomService, RoomService>();

            //Register Repository here
            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<IBookingHistoryRepository, BookingHistoryRepository>();
            services.AddSingleton<IRoomRepository, RoomRepository>();

            //Register Window
            services.AddTransient<LoginWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var loginWindow = ServiceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
        }
    }

}
