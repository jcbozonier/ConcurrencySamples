﻿using System;
using System.Text.RegularExpressions;
using EmailScraperNetwork.ChannelContracts;

namespace EmailScraperNetwork.Actors
{
    public class ObviousEmailExtractionAgent : INonblankLineOfTextChannel
    {
        private readonly IGoodEmailChannel GoodEmailChannel;
        private readonly IBadEmailChannel BadEmailChannel;
        private readonly Regex EmailMatcher;

        public ObviousEmailExtractionAgent(IGoodEmailChannel goodEmailChannel, IBadEmailChannel badEmailChannel)
        {
            GoodEmailChannel = goodEmailChannel;
            BadEmailChannel = badEmailChannel;

            EmailMatcher = new Regex(@"\b[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}\b");
        }

        public void OnNext(string lineOfText)
        {
            var matches = EmailMatcher.Matches(lineOfText);

            if(matches.Count > 0)
                GoodEmailChannel.OnNext(matches[0].Value);
            else 
                BadEmailChannel.OnNext(lineOfText);
        }

        public void OnComplete()
        {
            GoodEmailChannel.OnComplete();
        }
    }
}