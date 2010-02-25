using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace WebsiteIndexer.Actors
{
    public class WebsiteDownloader
    {
        readonly IInboundChannel<IMessage> InboundChannel;
        readonly IOutboundChannel<IMessage> OutboundChannel;
        bool Done;

        public WebsiteDownloader(IInboundChannel<IMessage> inboundChannel, IOutboundChannel<IMessage> outboundChannel)
        {
            InboundChannel = inboundChannel;
            OutboundChannel = outboundChannel;
        }

        public void Process()
        {
            while(!Done)
            {
                var message = InboundChannel.Receive();

                if(message is TerminateProcessMessage)
                    Done = true;

                if(message is UrlMessage)
                {
                    _GetPageSource(((UrlMessage)message).Url);
                }
            }
           
        }

        void _GetPageSource(string url)
        {
            var req = HttpWebRequest.Create(url);
            req.BeginGetResponse((ar) => _SendPageSourceAlong(req, ar), null);
        }

        void _SendPageSourceAlong(WebRequest req, IAsyncResult ar) 
        {
            var response = req.EndGetResponse(ar);
            var resp = response.GetResponseStream();
            using (var sr = new StreamReader(resp))
            {
                var webPageSourceMessage = new WebPageSourceMessage(sr.ReadToEnd());
                OutboundChannel.Send(webPageSourceMessage);
            }
        }
    }

    public class WebPageSourceMessage : IMessage
    {
        readonly string PageSource;

        public WebPageSourceMessage(string pageSource)
        {
            PageSource = pageSource;
        }
    }

    public class TerminateProcessMessage : IMessage
    {
        
    }

    public class UrlMessage : IMessage
    {
        public string Url;
    }

    public interface IObservable<T>
    {
        void Subscribe(T observer);
    }

    public interface IMessage
    {
        
    }

    public interface IInboundChannel<T>
    {
        T Receive();
    }

    public interface IOutboundChannel<T>
    {
        void Send(T message);
    }
}
