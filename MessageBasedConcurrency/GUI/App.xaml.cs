using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using MessageBasedConcurrency;
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
            var statusViewerViewModel = new StatusViewerViewModel();
            var statusViewer = new SavedWebsiteEdgesViewer();
            statusViewer.DataContext = statusViewerViewModel;

            statusViewer.Show();

            var displayTheSavedEdges = new DisplaySavedEdgesProcess(statusViewerViewModel, Dispatcher);
            new ParseLinksFromWebSiteProcess().DO(displayTheSavedEdges);

        }
    }
}
