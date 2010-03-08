using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using MessageBasedConcurrency.WebsiteDownloadExample;

namespace GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            new ParseLinksFromWebSiteProcess().DO();
        }
    }
}
