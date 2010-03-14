using System.Collections.Generic;
using EmailScraperNetwork.BaseFramework;

namespace EmailScraperAgentBehaviours.Agents.Fakes
{
    public class FileReaderWithOneNonBlankLineAndMultipleWhitespaceLines : IEachableFile<string>
    {
        public string ProvidedFilePath;

        public List<string> LinesInFile = new List<string>
                                              {
                                                  "\n",
                                                  "line 1",
                                                  "",
                                                  "\t ",
                                              };

        public IEnumerable<string> ReadFrom(string filePath)
        {
            ProvidedFilePath = filePath;

            return LinesInFile;
        }
    }
}