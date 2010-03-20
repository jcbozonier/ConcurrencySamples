using System;
using System.Linq;
using EmailScraperNetwork.BaseFramework;
using EmailScraperNetwork.ChannelContracts;

namespace EmailScraperNetwork.Actors
{
    public class LineByLineFileReadingAgentChannel : ILineByLineFileReadingAgentChannel
    {
        private readonly IFileReadingService _fileReadingServiceReader;
        private readonly INonblankLineOfTextChannel ChannelToSendNonBlankLinesOfTextTo;

        public LineByLineFileReadingAgentChannel(
            IFileReadingService fileReadingServiceReader, 
            INonblankLineOfTextChannel sendNonBlankLinesOfTextChannel)
        {
            _fileReadingServiceReader = fileReadingServiceReader;
            ChannelToSendNonBlankLinesOfTextTo = sendNonBlankLinesOfTextChannel;
        }

        public void SendBeginReadingFromFilePath(string filePath)
        {
            var lines = _fileReadingServiceReader.ReadFrom(filePath);

            foreach(var line in lines.Where(x=> x.Trim() != ""))
            {
                ChannelToSendNonBlankLinesOfTextTo.SendNonBlankLineOfText(line);
            }
        }
    }
}


