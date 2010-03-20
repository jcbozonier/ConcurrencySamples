using System;
using EmailScraperNetwork.ChannelContracts;

namespace EmailScraper
{
    public class DeadBadEmailChannel : IBadEmailChannel
    {
        public void OnNext(string emailAddress)
        {
        }

        public void OnComplete(string emailAddress)
        {
            
        }

        public void OnComplete()
        {
            
        }
    }
}