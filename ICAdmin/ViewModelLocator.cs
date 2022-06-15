using ICAdmin.Services;
using ICAdmin.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAdmin
{
    public class ViewModelLocator
    {
        private static ServiceProvider _provider;

        public static void Init()
        {
            var services = new ServiceCollection();

            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegistrationViewModel>();
            services.AddScoped<MainMenuViewModel>();
            services.AddScoped<ChatViewModel>();

            services.AddSingleton<PageService>();
            services.AddSingleton<EventBus>();
            services.AddSingleton<MessageBus>();
            services.AddSingleton<CheckConnectionService>();
            services.AddSingleton<AuthorizationService>();
            services.AddScoped<RegistrationService>();
            services.AddScoped<AnyDeskService>();
            services.AddScoped<UserMachineService>();


            _provider = services.BuildServiceProvider();

            foreach (var item in services)
            {
                _provider.GetRequiredService(item.ServiceType);
            }
        }

        public MainWindowViewModel MainWindowViewModel => _provider.GetRequiredService<MainWindowViewModel>();
        public LoginViewModel LoginViewModel => _provider.GetRequiredService<LoginViewModel>();
        public RegistrationViewModel RegistrationViewModel => _provider.GetRequiredService<RegistrationViewModel>();
        public MainMenuViewModel MainMenuViewModel => _provider.GetRequiredService<MainMenuViewModel>();
        public ChatViewModel ChatViewModel => _provider.GetRequiredService<ChatViewModel>();
    }

}
