using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using MessageBasedConcurrency.BaseFramework;
using MessageBasedConcurrency.WebsiteDownloadExample.Messages;

namespace MessageBasedConcurrency.WebsiteDownloadExample.Actors
{
    public class WebsiteDownloader : IObserver<UrlMessage>
    {
        private IObserver<WebPageSourceMessage> _PageParsingChannel;

        private bool _DomainSet;
        private string _DomainUri;

        public void OnNext(UrlMessage message)
        {
            try
            {
                var url = message.Url;

                if(!_DomainSet)
                    _GetDomainUri(url);

                if (_DomainSet && url.Contains(_DomainUri))
                {
                    var request = HttpWebRequest.Create(url);
                    request.BeginGetResponse(asyncResult => _SendPageSourceAlong(request, asyncResult, new Url(url)),
                                             null);
                }
            }
            catch(Exception err)
            {
                // Fuggit about it.
            }
        }

        public void _GetDomainUri(string url)
        {
            var match = Regex.Match(url, @"^http://[a-zA-Z0-9\/\.\-]+[\/]*$");
            if (match.Success)
            {
                _DomainUri = match.Value;
                _DomainSet = true;
            }
        }

        public void OnComplete()
        {

        }

        void _SendPageSourceAlong(WebRequest req, IAsyncResult ar, Url url)
        {
            try
            {
                var response = req.EndGetResponse(ar);
                var resp = response.GetResponseStream();
                using (var sr = new StreamReader(resp))
                {
                    var webPageSourceText = sr.ReadToEnd();
                    var webPageSourceMessage = new WebPageSourceMessage(url, webPageSourceText);
                    _PageParsingChannel.OnNext(webPageSourceMessage);
                }
            }
            catch
            {
                
            }
        }

        public void SendWebPageTextTo(IObserver<WebPageSourceMessage> webPageTextToParsedWebPage)
        {
            _PageParsingChannel = webPageTextToParsedWebPage;
        }
    }
}


