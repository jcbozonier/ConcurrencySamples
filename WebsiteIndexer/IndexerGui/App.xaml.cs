using System.Windows;
using MessageBasedConcurrency;
using MessageBasedConcurrency.WebsiteDownloadExample;

namespace IndexerGui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Startup += (sndr, o) =>
                           {
                               var vm = new StatusViewerViewModel();
                               var window = new Window1();
                               window.DataContext = vm;

                               window.Show();

                               var process = new ParseLinksFromWebSiteProcess();
                               var displaySavedEdgesProcess = new DisplaySavedEdgesProcess(vm, Dispatcher);
                               process.DO(displaySavedEdgesProcess);
                           };
        }
    }
}
