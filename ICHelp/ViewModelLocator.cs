using ICHelp.Services;
using ICHelp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICHelp
{
    public class ViewModelLocator
    {
        private static ServiceProvider _provider;

        public static void Init()
        {
            var services = new ServiceCollection();

            services.AddTransient<MainPageViewModel>();
           
            services.AddSingleton<PageService>();
            services.AddSingleton<CheckConnectionService>();
            services.AddSingleton<AuthorizationService>();
            services.AddScoped<RegistrationService>();
            services.AddSingleton<AssignmentService>();
            services.AddSingleton<AnyDeskService>();


            _provider = services.BuildServiceProvider();

            foreach (var item in services)
            {
                _provider.GetRequiredService(item.ServiceType);
            }
        }

        public MainPageViewModel MainPageViewModel => _provider.GetRequiredService<MainPageViewModel>();
    }

}
