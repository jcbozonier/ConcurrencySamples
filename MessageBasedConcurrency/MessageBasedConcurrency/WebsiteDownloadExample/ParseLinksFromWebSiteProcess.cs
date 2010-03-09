using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Threading;
using MessageBasedConcurrency.BaseFramework;
using MessageBasedConcurrency.WebsiteDownloadExample.Actors;
using MessageBasedConcurrency.WebsiteDownloadExample.Messages;

namespace MessageBasedConcurrency.WebsiteDownloadExample
{
    public class ParseLinksFromWebSiteProcess
    {
        public void DO(DisplaySavedEdgesProcess displaySavedEdgesProcess)
        {
            var urlToWebPageTextChannel = new Channel<UrlMessage>();
            var webPageTextToParsedWebPageChannel = new Channel<WebPageSourceMessage>();
            var parsedWebPageToDataStoreChannel = new Channel<ParsedWebPageMessage>();
            var parsedWebPageMessagesChannel = new Channel<ParsedWebPageMessage>();
            var displaySavedEdgeChannel = new Channel<WebsiteEdge>();

            var downloadWebpageProcess = new WebsiteDownloader();
            var parseWebPageProcess = new WebPageLinkParser();
            var messageCounterProcess = new MessageCounterProcess();
            var storeWebPageDataProcess = new WebPageDataStorer();

            urlToWebPageTextChannel.BroadcastTo(downloadWebpageProcess);
            downloadWebpageProcess.SendWebPageTextTo(webPageTextToParsedWebPageChannel);
            webPageTextToParsedWebPageChannel.BroadcastTo(parseWebPageProcess);
            parseWebPageProcess.SendParsedPagesTo(parsedWebPageToDataStoreChannel);
            parsedWebPageToDataStoreChannel.BroadcastTo(messageCounterProcess);
            messageCounterProcess.SendMessagesTo(parsedWebPageMessagesChannel);
            parsedWebPageMessagesChannel.BroadcastTo(storeWebPageDataProcess);
            storeWebPageDataProcess.SendUniqueUrlsTo(urlToWebPageTextChannel);
            storeWebPageDataProcess.SendSavedEdgesTo(displaySavedEdgeChannel);
            displaySavedEdgeChannel.BroadcastTo(displaySavedEdgesProcess);

            new IActivateableProcess[]
               {
                   urlToWebPageTextChannel, 
                   webPageTextToParsedWebPageChannel,
                   parsedWebPageToDataStoreChannel,
                   parsedWebPageMessagesChannel,
                   displaySavedEdgeChannel
               }.Start();

            urlToWebPageTextChannel.OnNext(new UrlMessage("http://www.cnn.com"));

            //downloadWebpageProcess.SendWebPageTextTo(parseWebPageProcess);
            //parseWebPageProcess.SendParsedPagesTo(messageCounter);
            //messageCounter.SendMessagesTo(storeWebPageDataProcess);
            //storeWebPageDataProcess.SendUniqueUrlsTo(downloadWebpageProcess);
            //storeWebPageDataProcess.SendSavedEdgesTo(displaySavedEdgesProcess);

            //downloadWebpageProcess.OnNext(new UrlMessage("http://www.cnn.com"));

        }
    }

    public class DisplaySavedEdgesProcess : IObserver<WebsiteEdge>
    {
        readonly StatusViewerViewModel ViewModel;
        readonly Dispatcher ViewDispatcher;

        public DisplaySavedEdgesProcess(StatusViewerViewModel viewModel, Dispatcher viewDispatcher)
        {
            ViewModel = viewModel;
            ViewDispatcher = viewDispatcher;
        }

        public void OnNext(WebsiteEdge message)
        {
            ViewDispatcher.Invoke((Action)(()=>ViewModel.WebsiteEdges.Add(message)), DispatcherPriority.Normal);
            
        }

        public void OnComplete()
        {
            
        }
    }

    public static class ThreadingExtensions
    {
        public static void Start(this IEnumerable<IActivateableProcess> processes)
        {
            processes.ToList().ForEach(x => ThreadPool.QueueUserWorkItem(y => x.Activate()));
        }
    }
}
