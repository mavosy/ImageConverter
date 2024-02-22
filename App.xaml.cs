using ImageToGrayscale.Models;
using ImageToGrayscale.ViewModels;
using ImageToGrayscale.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace ImageToGrayscale
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ImageProcessor>();
            services.AddTransient<ITGViewModel>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var itgView = new ITGView
            {
                DataContext = _serviceProvider.GetService<ITGViewModel>()
            };
            itgView.Show();
        }
    }
}