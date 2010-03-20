using System.Collections.ObjectModel;
using System.ComponentModel;
using EmailScraperNetwork.ChannelContracts;

namespace EmailScraper
{
    public class EmailScraperVM : INotifyPropertyChanged, IGoodEmailChannel
    {
        public EmailScraperVM()
        {
            EmailAddresses = new ObservableCollection<string>();
        }

        public ObservableCollection<string> EmailAddresses
        {
            get; 
            private set;
        }

        public void SendGoodEmailAddress(string emailAddress)
        {
            EmailAddresses.Add(emailAddress);
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("EmailAddresses"));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}