using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Compact;

namespace Dpz.Downloader.WinForm
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[]? args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            ApplicationConfiguration.Initialize();


            Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File(
                    new CompactJsonFormatter(),
                    Path.Combine("logs", $"{DateTime.Now:yyyyMM}", ".json"),
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 10240
                )
                .CreateLogger();


            var host = CreateHostBuilder(args).Build();
            var services = host.Services;

            Log.Information("application starting");
            Application.Run(services.GetRequiredService<Form1>());
        }


        private static IHostBuilder CreateHostBuilder(string[]? args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureServices((_, services) =>
                {
                    services.AddTransient<Form1>();                    
                    services.AddScoped(sp => new HttpClient());
                    services.AddLogging(builder => builder.AddSerilog());

#if DEBUG
                    services.AddBlazorWebViewDeveloperTools();
#endif
                    services.AddWindowsFormsBlazorWebView();

                    services.AddScoped<IServiceCollection>(_ => services);
                });
        }
    }
}