using System.Linq;
using MessageBasedConcurrency.BaseFramework;
using MessageBasedConcurrency.WebsiteDownloadExample.Messages;

namespace MessageBasedConcurrency.WebsiteDownloadExample.Actors
{
    public class MessageCounterProcess : IObserver<ParsedWebPageMessage>
    {
        int _Counter;

        IObserver<ParsedWebPageMessage> _NextChannel;

        public void OnNext(ParsedWebPageMessage message)
        {
            _Counter += message.ToUrls.Count();

            _NextChannel.OnNext(message);
        }

        public void OnComplete()
        {
            
        }

        public void SendMessagesTo(IObserver<ParsedWebPageMessage> channel)
        {
            _NextChannel = channel;
        }
    }
}
