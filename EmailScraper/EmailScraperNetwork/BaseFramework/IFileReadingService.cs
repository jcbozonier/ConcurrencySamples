using System;
using System.Collections.Generic;
using System.IO;

namespace EmailScraperNetwork.BaseFramework
{
    public interface IFileReadingService
    {
        IEnumerable<string> ReadFrom(string filePath);
    }

    public class FileReadingService : IFileReadingService
    {
        public IEnumerable<string> ReadFrom(string filePath)
        {
            using (var file = new StreamReader(filePath))
                while (!file.EndOfStream)
                    yield return file.ReadLine();
        }
    }
}