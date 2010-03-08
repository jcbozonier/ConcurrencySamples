using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebsiteIndexer.Actors.Reactive
{
    public class WebsiteDownloader : IActor<IMessage>
    {
        readonly ISendChannel<IMessage> SendChannel;

        public WebsiteDownloader(ISendChannel<IMessage> sendChannel)
        {
            SendChannel = sendChannel;
        }

        public void OnNext(IMessage message)
        {
            // Process
            SendChannel.Send(message);
        }
    }

    public class Channel<T> : ISendChannel<T>
    {
        IActor<IMessage> _ObservingActor;
        Queue _MessageQueue;

        public Channel()
        {
            var queue = new Queue();
            _MessageQueue = Queue.Synchronized(queue);
        }

        public void Activate()
        {
            // Start spinning the thread... pull from internal queue

            while(true)
            {
                _ObservingActor.OnNext(null);
            }
        }


        public void Send(T message)
        {
            
        }
    }

    interface IActor<T>
    {
        void OnNext(T message);
    }
}
