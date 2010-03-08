using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MessageBasedConcurrency.BaseFramework;
using MessageBasedConcurrency.WebsiteDownloadExample.Messages;

namespace MessageBasedConcurrency.WebsiteDownloadExample.Actors
{
    public class WebPageDataStorer : IObserver<ParsedWebPageMessage>
    {
        private IObserver<UrlMessage> _NewlyEnteredUrlNotifications;

        readonly List<WebsiteEdge> _WebsiteEdges;

        public WebPageDataStorer()
        {
            _WebsiteEdges = new List<WebsiteEdge>();
        }

        public void OnNext(ParsedWebPageMessage message)
        {
            var websiteEdges = message.ToUrls.Select(toUrl =>
                                                     {
                                                         
                                                         var websiteEdge = new WebsiteEdge(message.FromUrl, toUrl);
                                                         return websiteEdge;
                                                     });
            _WebsiteEdges.AddRange(websiteEdges);

            foreach(var url in message.ToUrls)
            {
                _NewlyEnteredUrlNotifications.OnNext(new UrlMessage(url));
                Debug.WriteLine(url);
            }
        }

        public void OnComplete()
        {
            
        }

        public void SendUniqueUrlsTo(IObserver<UrlMessage> channel)
        {
            _NewlyEnteredUrlNotifications = channel;
        }
    }

    public class WebsiteEdge
    {
        public WebsiteEdge(Url fromUrl, string toUrl)
        {
            
        }
    }
}