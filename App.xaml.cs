using ImageToGrayscale.Services;
using ImageToGrayscale.Services.Interfaces;
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
            services.AddTransient<ITGViewModel>();
            services.AddTransient<IImageProcessor, ImageProcessor>();
            services.AddTransient<IDialogService, DialogService>();
            services.AddTransient<IFileProcessingService, FileProcessingService>();
            services.AddTransient<IMessageService, MessageService>();
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