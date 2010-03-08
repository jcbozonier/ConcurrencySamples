using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MessageBasedConcurrency.BaseFramework;
using MessageBasedConcurrency.WebsiteDownloadExample.Actors;
using MessageBasedConcurrency.WebsiteDownloadExample.Messages;

namespace MessageBasedConcurrency.WebsiteDownloadExample
{
    public class ParseLinksFromWebSiteProcess
    {
        public void DO()
        {
            var urlToWebPageTextChannel = new Channel<UrlMessage>();
            var webPageTextToParsedWebPageChannel = new Channel<WebPageSourceMessage>();
            var parsedWebPageToDataStoreChannel = new Channel<ParsedWebPageMessage>();

            var downloadWebpageProcess = new WebsiteDownloader();
            var parseWebPageProcess = new WebPageLinkParser();
            var storeWebPageDataProcess = new WebPageDataStorer();

            urlToWebPageTextChannel.BroadcastTo(downloadWebpageProcess);
            downloadWebpageProcess.SendWebPageTextTo(webPageTextToParsedWebPageChannel);
            webPageTextToParsedWebPageChannel.BroadcastTo(parseWebPageProcess);
            parseWebPageProcess.SendParsedPagesTo(parsedWebPageToDataStoreChannel);
            parsedWebPageToDataStoreChannel.BroadcastTo(storeWebPageDataProcess);
            storeWebPageDataProcess.SendUniqueUrlsTo(urlToWebPageTextChannel);

            new IActivateableProcess[]
               {
                   urlToWebPageTextChannel, 
                   webPageTextToParsedWebPageChannel,
                   parsedWebPageToDataStoreChannel
               }.Start();

            urlToWebPageTextChannel.OnNext(new UrlMessage("http://www.cnn.com"));

            //downloadWebpageProcess.SendWebPageTextTo(parseWebPageProcess);
            //parseWebPageProcess.SendParsedPagesTo(storeWebPageDataProcess);
            //storeWebPageDataProcess.SendUniqueUrlsTo(downloadWebpageProcess);

            //downloadWebpageProcess.OnNext(new UrlMessage("http://www.cnn.com"));

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
