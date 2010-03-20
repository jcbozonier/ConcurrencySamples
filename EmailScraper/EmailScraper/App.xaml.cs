using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using EmailScraperNetwork.Actors;
using EmailScraperNetwork.BaseFramework;

namespace EmailScraper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Startup += (x, y) =>
                                 {
                                     var router = new ProcessRouter();
                                     var fileReadingServiceReader = new FileReadingService();
                                     var lineByLineFileReadingAgent = new LineByLineFileReadingAgentChannel(fileReadingServiceReader, router);
                                     var sendNonBlankLinesHere = new ObviousEmailExtractionAgent(router, router);
                                     var sendGoodEmailsHere = new EmailScraperVM(router);

                                     router.SetBadEmailChannel(new DeadBadEmailChannel());
                                     router.SetGoodEmailChannel(sendGoodEmailsHere);
                                     router.SetNonBlankLineOfTextChannel(sendNonBlankLinesHere);
                                     router.SetLineByLineFileReadingChannel(lineByLineFileReadingAgent);

                                     new Window1()
                                         {
                                             DataContext = sendGoodEmailsHere,
                                         }.Show();

                                     sendGoodEmailsHere.StartFileReading(@"C:\Code\ConcurrencySamples\EmailScraper\Files\Sample1.txt");
                                 };
        }
    }
}
