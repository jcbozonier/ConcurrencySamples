using System.Collections.Generic;

namespace EmailScraperNetwork.BaseFramework
{
    public interface IReadableFile<T>
    {
        IEnumerable<T> ReadFrom(string filePath);
    }
}