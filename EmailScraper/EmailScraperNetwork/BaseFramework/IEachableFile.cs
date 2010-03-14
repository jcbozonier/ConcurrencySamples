using System.Collections.Generic;

namespace EmailScraperNetwork.BaseFramework
{
    public interface IEachableFile<T>
    {
        IEnumerable<T> ReadFrom(string filePath);
    }
}