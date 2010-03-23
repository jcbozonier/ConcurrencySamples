using System.Collections.Generic;
using System.ComponentModel;
using MessageBasedConcurrency.WebsiteDownloadExample.Actors;

namespace MessageBasedConcurrency
{
    public class StatusViewerViewModel : INotifyPropertyChanged
    {
        public StatusViewerViewModel()
        {
            WebsiteEdges = new List<WebsiteEdge>();
        }

        public List<WebsiteEdge> WebsiteEdges { get; private set; }
        public int EdgeCount { get; private set; }

        public void AddEdge(WebsiteEdge edge)
        {
            WebsiteEdges.Add(edge);
            EdgeCount++;

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("WebsiteEdges"));
                PropertyChanged(this, new PropertyChangedEventArgs("EdgeCount"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
