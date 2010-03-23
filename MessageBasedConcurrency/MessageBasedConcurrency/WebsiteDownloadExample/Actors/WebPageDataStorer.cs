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
        private IObserver<WebsiteEdge> _NewlySavedEdge;

        readonly List<WebsiteEdge> _WebsiteEdges;
        readonly HashSet<string> _WebsiteAddresses;

        public WebPageDataStorer()
        {
            _WebsiteEdges = new List<WebsiteEdge>();
            _WebsiteAddresses = new HashSet<string>();
        }

        public void OnNext(ParsedWebPageMessage message)
        {
            var websiteEdges = message.ToUrls.Select(toUrl =>
                                                     {
                                                         
                                                         var websiteEdge = new WebsiteEdge(message.FromUrl, toUrl);
                                                         return websiteEdge;
                                                     });
            _WebsiteEdges.AddRange(websiteEdges);

            foreach(var edge in websiteEdges)
            {
                _NewlySavedEdge.OnNext(edge);
            }

            foreach(var url in message.ToUrls)
            {
                if (!_WebsiteAddresses.Contains(url))
                {
                    _NewlyEnteredUrlNotifications.OnNext(new UrlMessage(url));
                    _WebsiteAddresses.Add(url);
                }
            }
        }

        public void OnComplete()
        {
            
        }

        public void SendUniqueUrlsTo(IObserver<UrlMessage> channel)
        {
            _NewlyEnteredUrlNotifications = channel;
        }

        public void SendSavedEdgesTo(IObserver<WebsiteEdge> notifySavedEdges)
        {
            _NewlySavedEdge = notifySavedEdges;
        }
    }
}