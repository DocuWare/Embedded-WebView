using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using System;
using System.Windows;

namespace EmbeddedWebView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string originalTitle;

        public MainWindow()
        {
            InitializeComponent();

            originalTitle = Title;

            webView.Source = new Uri(System.Configuration.ConfigurationManager.AppSettings.Get("Source"));
            webView.WebMessageReceived += WebView_WebMessageReceived;
        }

        private void WebView_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.WebMessageAsJson))
            {
                dynamic message = JsonConvert.DeserializeObject(e.WebMessageAsJson);
                if (message.action == "setTitle")
                {
                    Title = $"{originalTitle} {message.value}";
                }

                if (message.action == "revertTitle")
                {
                    Title = originalTitle;
                }
            }
        }
    }
}
