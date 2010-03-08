using System.Collections.Generic;
using MessageBasedConcurrency.WebsiteDownloadExample.Actors;

namespace MessageBasedConcurrency.WebsiteDownloadExample.Messages
{
    public class ParsedWebPageMessage
    {
        public Url FromUrl { get; private set;}
        public IEnumerable<string> ToUrls { get; private set; }

        public ParsedWebPageMessage(Url fromUrl, IEnumerable<string> toUrls)
        {
            FromUrl = fromUrl;
            ToUrls = toUrls;
        }
    }
}