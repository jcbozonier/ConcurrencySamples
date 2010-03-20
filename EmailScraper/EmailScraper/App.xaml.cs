using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using EmailScraperNetwork.Actors;
using EmailScraperNetwork.BaseFramework;
using EmailScraperNetwork.ChannelContracts;

namespace EmailScraper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Startup += (x, y) => _RunApp();
        }

        private static void _RunApp()
        {
            var ui = new Window1();

            var router = new ProcessRouter();
            var fileReadingServiceReader = new FileReadingService();
            var lineByLineFileReadingAgent = new LineByLineFileReadingAgentChannel(fileReadingServiceReader, router);
            var emailExtractionAgent = new ObviousEmailExtractionAgent(router, router);
            var vm = new EmailScraperVM();
            var user = new ThreadableVM(vm, ui.Dispatcher);

            router.SendLinesOfTextWithNoObviousEmailAddressTo(new DeadBadEmailChannel());
            router.SendGoodEmailAddressesTo(user);
            router.SendNonBlankLineOfTextTo(emailExtractionAgent);
            router.SendFilesToReadFromTo(lineByLineFileReadingAgent);

            router.StartProcess(@"C:\Code\ConcurrencySamples\EmailScraper\Files\Sample1.txt");

            ui.DataContext = vm;
            ui.Show();
        }
    }

    internal class ThreadableVM : IGoodEmailChannel
    {
        private readonly IGoodEmailChannel _goodEmailChannel;
        private readonly Dispatcher _dispatcher;

        public ThreadableVM(IGoodEmailChannel goodEmailChannel, Dispatcher dispatcher)
        {
            _goodEmailChannel = goodEmailChannel;
            _dispatcher = dispatcher;
        }

        public void SendGoodEmailAddress(string emailAddress)
        {
            _dispatcher.Invoke((Action)(() => _goodEmailChannel.SendGoodEmailAddress(emailAddress)));
        }
    }
}
