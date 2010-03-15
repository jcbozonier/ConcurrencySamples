using System.Linq;
using EmailScraperNetwork.BaseFramework;

namespace EmailScraperNetwork.Actors
{
    public class LineByLineFileReadingAgent : IObserver<string>
    {
        private readonly IReadableFile<string> FileReader;
        private IObserver<string> ChannelToSendNonBlankLinesOfTextTo;

        public LineByLineFileReadingAgent(IReadableFile<string> fileReader)
        {
            FileReader = fileReader;
        }

        public void ShouldSendLinesOfTextTo(IObserver<string> channel)
        {
            ChannelToSendNonBlankLinesOfTextTo = channel;
        }

        public void OnNext(string filePath)
        {
            var lines = FileReader.ReadFrom(filePath);

            foreach(var line in lines.Where(x=> x.Trim() != ""))
            {
                ChannelToSendNonBlankLinesOfTextTo.OnNext(line);
            }
        }
    }
}


