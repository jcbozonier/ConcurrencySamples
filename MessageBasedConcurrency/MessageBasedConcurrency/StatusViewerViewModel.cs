using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MessageBasedConcurrency.WebsiteDownloadExample.Actors;

namespace MessageBasedConcurrency
{
    public class StatusViewerViewModel : INotifyPropertyChanged
    {
        public StatusViewerViewModel()
        {
            WebsiteEdges = new ObservableCollection<WebsiteEdge>();
        }

        public ObservableCollection<WebsiteEdge> WebsiteEdges { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
