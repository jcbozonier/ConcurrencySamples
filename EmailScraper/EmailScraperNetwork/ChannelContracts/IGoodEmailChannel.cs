using EmailScraperNetwork.BaseFramework;

namespace EmailScraperNetwork.ChannelContracts
{
    public interface IGoodEmailChannel : IObserver<string>
    {
        void OnNext(string emailAddress);
        void OnComplete();
    }
}