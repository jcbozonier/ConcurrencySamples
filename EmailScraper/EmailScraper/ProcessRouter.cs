using System;
using System.Threading;
using EmailScraperNetwork.ChannelContracts;

namespace EmailScraper
{
    public class ProcessRouter : INonblankLineOfTextChannel, IGoodEmailChannel, IBadEmailChannel, ILineByLineFileReadingAgentChannel
    {
        private ILineByLineFileReadingAgentChannel _lineByLineFileReadingAgentChannel;
        private INonblankLineOfTextChannel _NonblankLineOfTextChannel;
        private IGoodEmailChannel _GoodEmailChannel;
        private IBadEmailChannel _BadEmailChannel;

        public void SendNonBlankLineOfTextTo(INonblankLineOfTextChannel nonblankLineOfTextChannel)
        {
            _NonblankLineOfTextChannel = nonblankLineOfTextChannel;
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
            _lineByLineFileReadingAgentChannel = lineByLineFileReadingAgentChannel;
        }

        public void SendNonBlankLineOfText(string message)
        {
            _NonblankLineOfTextChannel.SendNonBlankLineOfText(message);
        }

        public void SendGoodEmailAddress(string emailAddress)
        {
            _GoodEmailChannel.SendGoodEmailAddress(emailAddress);
        }

        public void SendBadEmailAddress(string emailAddress)
        {
            _BadEmailChannel.SendBadEmailAddress(emailAddress);
        }

        public void SendBeginReadingFromFilePath(string filePath)
        {
            _lineByLineFileReadingAgentChannel.SendBeginReadingFromFilePath(filePath);
        }

        public void StartFileReading(string filePath)
        {
            _lineByLineFileReadingAgentChannel.SendBeginReadingFromFilePath(filePath);
        }

        public void StartProcess(string filePath)
        {
            ThreadPool.QueueUserWorkItem(x => _lineByLineFileReadingAgentChannel.SendBeginReadingFromFilePath(filePath));
        }
    }
}