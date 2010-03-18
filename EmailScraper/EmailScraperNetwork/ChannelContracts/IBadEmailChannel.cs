namespace EmailScraperNetwork.ChannelContracts
{
    public interface IBadEmailChannel
    {
        void SendBadEmailAddress(string emailAddress);
    }
}