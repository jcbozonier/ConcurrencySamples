using System.Collections.Generic;
using System.Linq;
using EmailScraperNetwork.BaseFramework;

namespace EmailScraperAgentBehaviours.Agents.Fakes
{
    public class FileReadingServiceReaderWithOneNonBlankLineOfText : IFileReadingService
    {
        public string FilePath;
        public const int NonblankLineCount = 1;

        public IEnumerable<string> ReadFrom(string filePath)
        {
            FilePath = filePath;

            return Enumerable.Range(0, NonblankLineCount).Select(x => x.ToString());
        }
    }
}