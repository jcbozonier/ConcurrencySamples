namespace MessageBasedConcurrency.WebsiteDownloadExample.Actors
{
    public class Url
    {
        public string Value{ get; private set;}

        public Url(string url)
        {
            Value = url;
        }
    }
}