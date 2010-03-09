namespace MessageBasedConcurrency.WebsiteDownloadExample.Actors
{
    public class WebsiteEdge
    {
        public string FromUrl { get; private set; }
        public string ToUrl { get; private set; }

        public WebsiteEdge(Url fromUrl, string toUrl)
        {
            FromUrl = fromUrl.Value;
            ToUrl = toUrl;
        }
    }
}