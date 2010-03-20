using EmailScraperNetwork.BaseFramework;

namespace EmailScraperNetwork.ChannelContracts
{
    public interface IBadEmailChannel : IObserver<string>
    {
        void OnNext(string emailAddress);
        void OnComplete(string emailAddress);
    }
}