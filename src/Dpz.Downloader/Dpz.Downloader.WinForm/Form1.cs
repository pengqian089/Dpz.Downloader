using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dpz.Downloader.WinForm
{
    public partial class Form1 : Form
    {
        public Form1(IServiceCollection services, ILogger<Form1> logger)
        {
            InitializeComponent();


            services.AddTransient<SettingsForm>();
            blazorWebView1.HostPage = "wwwroot\\index.html";
            blazorWebView1.Services = services.BuildServiceProvider();

            logger.LogInformation("open Form1");

            blazorWebView1.RootComponents.Add<HeadOutlet>("head::after");
            blazorWebView1.RootComponents.Add<App>("#app");
        }
    }
}
