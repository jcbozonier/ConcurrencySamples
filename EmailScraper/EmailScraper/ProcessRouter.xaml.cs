using EmailScraperNetwork.ChannelContracts;

namespace EmailScraper
{
    public class ProcessRouter : INonblankLineOfTextChannel, IGoodEmailChannel, IBadEmailChannel, ILineByLineFileReadingAgentChannel
    {
        private ILineByLineFileReadingAgentChannel _lineByLineFileReadingAgentChannel;
        private INonblankLineOfTextChannel _NonblankLineOfTextChannel;
        private IGoodEmailChannel _GoodEmailChannel;
        private IBadEmailChannel _BadEmailChannel;

        public void SetNonBlankLineOfTextChannel(INonblankLineOfTextChannel nonblankLineOfTextChannel)
        {
            _NonblankLineOfTextChannel = nonblankLineOfTextChannel;
        }

        public void SetGoodEmailChannel(IGoodEmailChannel channel)
        {
            _GoodEmailChannel = channel;
        }

        public void SetBadEmailChannel(IBadEmailChannel channel)
        {
            _BadEmailChannel = channel;
        }

        public void SetLineByLineFileReadingChannel(ILineByLineFileReadingAgentChannel lineByLineFileReadingAgentChannel)
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
    }
}