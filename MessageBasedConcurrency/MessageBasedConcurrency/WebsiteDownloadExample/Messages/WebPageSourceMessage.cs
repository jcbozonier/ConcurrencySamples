using MessageBasedConcurrency.WebsiteDownloadExample.Actors;

namespace MessageBasedConcurrency.WebsiteDownloadExample.Messages
{
    public class WebPageSourceMessage
    {
        readonly Url _Url;
        private readonly string _WebPageSourceText;

        public WebPageSourceMessage(Url url, string _webPageSourceText)
        {
            _Url = url;
            _WebPageSourceText = _webPageSourceText;
        }

        public Url Url
        {
            get { return _Url; }
        }

        public string WebPageSourceText
        {
            get { return _WebPageSourceText; }
        }
    }
}