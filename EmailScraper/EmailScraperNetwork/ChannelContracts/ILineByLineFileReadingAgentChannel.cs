using System;
using EmailScraperNetwork.BaseFramework;

namespace EmailScraperNetwork.ChannelContracts
{
    public interface ILineByLineFileReadingAgentChannel : IObserver<string>
    {
        void OnNext(string message);
        void OnComplete();
    }
}
