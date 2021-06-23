using Microsoft.Extensions.DependencyInjection;
using Somerpg.Client.Service;
using Somerpg.Client.ViewModel;
using Somerpg.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Somerpg
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();
            RegisterServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void RegisterServices(ServiceCollection services)
        {
            // todo: config offline mode with dummy service, online mode with actual one
            services.AddSingleton<IService>(new DummyService());

            services.AddSingleton<ITimerService>(new TimerService());
            services.AddSingleton<MainViewModel>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var viewModel = _serviceProvider.GetService<MainViewModel>();
            var window = new MainWindow(viewModel);
            window.Show();
        }
    }
}
