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

            services.AddTransient<MainWindowViewModel>();
            services.AddScoped<LoginViewModel>();
            services.AddScoped<RegistrationViewModel>();
            services.AddScoped<MainMenuViewModel>();

            services.AddSingleton<PageService>();
            services.AddSingleton<EventBus>();
            services.AddSingleton<MessageBus>();
            services.AddSingleton<CheckConnectionService>();
            services.AddSingleton<AuthorizationService>();
            services.AddScoped<RegistrationService>();



            _provider = services.BuildServiceProvider();

            foreach (var item in services)
            {
                _provider.GetRequiredService(item.ServiceType);
            }
        }

        public MainWindowViewModel MainWindowViewModel => _provider.GetRequiredService<MainWindowViewModel>();
        public LoginViewModel LoginViewModel => _provider.GetRequiredService<LoginViewModel>();
        public MainMenuViewModel MainMenuViewModel => _provider.GetRequiredService<MainMenuViewModel>();
    }

}
