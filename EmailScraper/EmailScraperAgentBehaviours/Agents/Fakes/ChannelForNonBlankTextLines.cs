﻿using System.Collections.Generic;
using EmailScraperNetwork.ChannelContracts;

namespace EmailScraperAgentBehaviours.Agents.Fakes
{
    public class ChannelForNonBlankTextLines : INonblankLineOfTextChannel
    {
        public int ReceivedMessagesCount;
        public readonly List<string> ReceivedMessages;

        public ChannelForNonBlankTextLines()
        {
            ReceivedMessages = new List<string>();
        }

        public void SendNonBlankLineOfText(string lineOfText)
        {
            ReceivedMessagesCount++;
            ReceivedMessages.Add(lineOfText);
        }
    }
}