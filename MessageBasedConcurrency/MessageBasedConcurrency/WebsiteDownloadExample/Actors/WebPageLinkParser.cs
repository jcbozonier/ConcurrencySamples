using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MessageBasedConcurrency.BaseFramework;
using MessageBasedConcurrency.WebsiteDownloadExample.Messages;

namespace MessageBasedConcurrency.WebsiteDownloadExample.Actors
{
    public class WebPageLinkParser : IObserver<WebPageSourceMessage>
    {
        private IObserver<ParsedWebPageMessage> _StoreWebPageDataProcess;

        public void OnNext(WebPageSourceMessage message)
        {
            var webPageSourceText = message.WebPageSourceText;

            var pageLinks = _GetLinks(webPageSourceText);

            var parsedLinks = new ParsedWebPageMessage(message.Url, pageLinks.Select(toUrl =>
                                                                                     {
                                                                                         var fullUrl = toUrl.Contains("http://")
                                                                                                         ? toUrl
                                                                                                         : message.Url.Value + toUrl;
                                                                                         return fullUrl;
                                                                                     }).ToArray());

            _StoreWebPageDataProcess.OnNext(parsedLinks);
        }

        static IEnumerable<string> _GetLinks(string webPageSourceText)
        {
            var matchlinks = @"]*?HREF\s*=\s*[""']?([^'"" >]+?)[ '""]?[^>]*?>";
            var regex = new Regex(matchlinks, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            var matches = regex.Matches(webPageSourceText);

            var urls = _GetUrls(matches).ToArray();

            return urls;
        }

        static IEnumerable<string> _GetUrls(MatchCollection matches)
        {
            foreach(Match match in matches)
            {
                var link = match.Value.ToLower();
                var linkLength = link.Length;
                var startPosition = link.IndexOf("href=") + 6;
                var endPosition = Enumerable.Range(startPosition, linkLength - startPosition)
                    .Aggregate((x,y) =>
                                link[x] == '\"' || link[x] == '\''
                                ? x 
                                : y);
                var url = link.Substring(startPosition, endPosition-startPosition);
                yield return url;
            }
        }

        public void OnComplete()
        {
            
        }

        public void SendParsedPagesTo(IObserver<ParsedWebPageMessage> storeWebPageDataProcess)
        {
            _StoreWebPageDataProcess = storeWebPageDataProcess;
        }
    }
}