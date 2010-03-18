using System;
using System.Collections.Generic;
using EmailScraperNetwork.ChannelContracts;

namespace EmailScraperAgentBehaviours.Agents.Fakes
{
    public class TransperantGoodEmailChannel : IGoodEmailChannel
    {
        public int ReceivedMessagesCount;
        public readonly List<string> ReceivedMessages;

        public TransperantGoodEmailChannel()
        {
            ReceivedMessages = new List<string>();
        }

        public void SendGoodEmailAddress(string lineOfText)
        {
            ReceivedMessagesCount++;
            ReceivedMessages.Add(lineOfText);
        }
    }

    public class TransperantBadEmailChannel : IBadEmailChannel
    {
        public int ReceivedMessagesCount;
        public readonly List<string> ReceivedMessages;

        public TransperantBadEmailChannel()
        {
            ReceivedMessages = new List<string>();
        }

        public void SendBadEmailAddress(string lineOfText)
        {
            ReceivedMessagesCount++;
            ReceivedMessages.Add(lineOfText);
        }
    }
}