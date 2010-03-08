using System;

namespace MessageBasedConcurrency.WebsiteDownloadExample.Messages
{
    public class UrlMessage
    {
        public UrlMessage(string urlString)
        {
            Url = urlString;
        }

        public string Url { get; private set; }
    }
}