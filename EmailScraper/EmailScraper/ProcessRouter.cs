using System;
using System.Threading;
using EmailScraperNetwork.BaseFramework;
using EmailScraperNetwork.ChannelContracts;

namespace EmailScraper
{
    public class ProcessRouter : INonblankLineOfTextChannel, IGoodEmailChannel, IBadEmailChannel, ILineByLineFileReadingAgentChannel
    {
        private readonly Channel<ILineByLineFileReadingAgentChannel, string> _LineByLineFileReadingAgentChannel;
        private readonly Channel<INonblankLineOfTextChannel, string> _NonblankLineOfTextChannel;
        private IGoodEmailChannel _GoodEmailChannel;
        private IBadEmailChannel _BadEmailChannel;

        public ProcessRouter()
        {
            _LineByLineFileReadingAgentChannel = new Channel<ILineByLineFileReadingAgentChannel, string>();
            _NonblankLineOfTextChannel = new Channel<INonblankLineOfTextChannel, string>();
        }

        public void SendNonBlankLineOfTextTo(INonblankLineOfTextChannel nonblankLineOfTextChannel)
        {
            _NonblankLineOfTextChannel.BroadcastTo(nonblankLineOfTextChannel);
        }

        public void SendGoodEmailAddressesTo(IGoodEmailChannel channel)
        {
            _GoodEmailChannel = channel;
        }

        public void SendLinesOfTextWithNoObviousEmailAddressTo(IBadEmailChannel channel)
        {
            _BadEmailChannel = channel;
        }

        public void SendFilesToReadFromTo(ILineByLineFileReadingAgentChannel lineByLineFileReadingAgentChannel)
        {
            _LineByLineFileReadingAgentChannel.BroadcastTo(lineByLineFileReadingAgentChannel);
        }

        void INonblankLineOfTextChannel.OnNext(string message)
        {
            _NonblankLineOfTextChannel.OnNext(message);
        }

        void INonblankLineOfTextChannel.OnComplete()
        {
            _NonblankLineOfTextChannel.OnComplete();
        }

        void IGoodEmailChannel.OnNext(string emailAddress)
        {
            _GoodEmailChannel.OnNext(emailAddress);
        }
        void IGoodEmailChannel.OnComplete()
        {
            _GoodEmailChannel.OnComplete();
        }

        void IBadEmailChannel.OnNext(string emailAddress)
        {
            _BadEmailChannel.OnNext(emailAddress);
        }
        void IBadEmailChannel.OnComplete(string emailAddress)
        {
            _BadEmailChannel.OnComplete();
        }

        void ILineByLineFileReadingAgentChannel.OnNext(string filePath)
        {
            _LineByLineFileReadingAgentChannel.OnNext(filePath);
        }
        void ILineByLineFileReadingAgentChannel.OnComplete()
        {
            _LineByLineFileReadingAgentChannel.OnComplete();
        }

        public void StartProcess(string filePath)
        {
            ThreadPool.QueueUserWorkItem(x => _LineByLineFileReadingAgentChannel.Activate());
            ThreadPool.QueueUserWorkItem(x => _NonblankLineOfTextChannel.Activate());

            _LineByLineFileReadingAgentChannel.OnNext(filePath);
            _LineByLineFileReadingAgentChannel.OnComplete();
        }

        public void OnNext(string message)
        {
            throw new NotImplementedException();
        }

        public void OnComplete()
        {
            throw new NotImplementedException();
        }
    }

    public class SyncChannel<T, K> : IObserver<K>
        where T : IObserver<K>
    {
        private T _Agent;

        public void OnNext(K message)
        {
            _Agent.OnNext(message);
        }

        public void OnComplete()
        {
            _Agent.OnComplete();
        }

        public void BroadcastTo(T agent)
        {
            _Agent = agent;
        }
    }
}