using System.Collections.Generic;
using EmailScraperNetwork.BaseFramework;

namespace EmailScraperAgentBehaviours.Agents.Fakes
{
    public class FileReaderWithOneNonBlankLineAndMultipleBlankLines : IEachableFile<string>
    {
        public string ProvidedFilePath;

        public List<string> LinesInFile = new List<string>
                                              {
                                                  "",
                                                  "line 1",
                                                  "",
                                              };

        public IEnumerable<string> ReadFrom(string filePath)
        {
            ProvidedFilePath = filePath;

            return LinesInFile;
        }
    }
}