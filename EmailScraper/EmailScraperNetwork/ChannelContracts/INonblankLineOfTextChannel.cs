using EmailScraperNetwork.BaseFramework;

namespace EmailScraperNetwork.ChannelContracts
{
    public interface INonblankLineOfTextChannel : IObserver<string>
    {
        void OnNext(string message);
        void OnComplete();
    }
}
