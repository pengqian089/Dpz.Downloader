using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dpz.Downloader.WinForm
{
    public partial class SettingsForm : Form
    {
        public SettingsForm(IServiceProvider serviceProvider, ILogger<SettingsForm> logger)
        {
            InitializeComponent();

            logger.LogInformation("open settings");

            blazorWebView1.HostPage = "wwwroot\\index.html";
            blazorWebView1.Services = serviceProvider;
            blazorWebView1.StartPath = "/settings";

            logger.LogInformation("open Form1");

            blazorWebView1.RootComponents.Add<HeadOutlet>("head::after");
            blazorWebView1.RootComponents.Add<App>("#app");
        }
    }
}
