namespace EmailScraperNetwork.ChannelContracts
{
    public interface ILineByLineFileReadingAgentChannel
    {
        void SendBeginReadingFromFilePath(string filePath);
    }
}