using System;
using System.Text.RegularExpressions;
using EmailScraperNetwork.BaseFramework;

namespace EmailScraperNetwork.Actors
{
    public class ObviousEmailExtractionAgent : IObserver<string>
    {
        private IObserver<string> GoodEmailChannel;
        private IObserver<string> BadEmailChannel;
        private readonly Regex EmailMatcher;

        public ObviousEmailExtractionAgent()
        {
            EmailMatcher = new Regex(@"\b[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}\b");
        }

        public void OnNext(string lineOfText)
        {
            var matches = EmailMatcher.Matches(lineOfText);

            if(matches.Count > 0)
                GoodEmailChannel.OnNext(matches[0].Value);
            else 
                BadEmailChannel.OnNext(lineOfText);
        }

        public void ShouldSendGoodEmailsTo(IObserver<string> channel)
        {
            GoodEmailChannel = channel;
        }

        public void ShouldSendBadEmailsTo(IObserver<string> channel)
        {
            BadEmailChannel = channel;
        }
    }
}