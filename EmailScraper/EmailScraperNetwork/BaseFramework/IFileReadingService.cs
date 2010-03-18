using System.Collections.Generic;

namespace EmailScraperNetwork.BaseFramework
{
    public interface IFileReadingService
    {
        IEnumerable<string> ReadFrom(string filePath);
    }
}