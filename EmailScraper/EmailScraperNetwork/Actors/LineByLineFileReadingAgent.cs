using System;
using System.Linq;
using EmailScraperNetwork.BaseFramework;
using EmailScraperNetwork.ChannelContracts;

namespace EmailScraperNetwork.Actors
{
    public class LineByLineFileReadingAgent
    {
        private readonly IFileReadingService _fileReadingServiceReader;
        private readonly INonblankLineOfTextChannel ChannelToSendNonBlankLinesOfTextTo;

        public LineByLineFileReadingAgent(
            IFileReadingService fileReadingServiceReader, 
            INonblankLineOfTextChannel sendNonBlankLinesOfTextChannel)
        {
            _fileReadingServiceReader = fileReadingServiceReader;
            ChannelToSendNonBlankLinesOfTextTo = sendNonBlankLinesOfTextChannel;
        }

        public void BeginReadingFromFilePath(string filePath)
        {
            var lines = _fileReadingServiceReader.ReadFrom(filePath);

            foreach(var line in lines.Where(x=> x.Trim() != ""))
            {
                ChannelToSendNonBlankLinesOfTextTo.SendNonBlankLineOfText(line);
            }
        }
    }
}


