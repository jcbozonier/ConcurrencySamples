using System;
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

        public int EmailAddressCount
        {
            get; 
            private set;
        }

        void IGoodEmailChannel.OnNext(string emailAddress)
        {
            EmailAddresses.Add(emailAddress);
            EmailAddressCount++;

            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("EmailAddresses"));
                PropertyChanged(this, new PropertyChangedEventArgs("EmailAddressCount"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnNext(string message)
        {
            throw new NotImplementedException();
        }

        public void OnComplete()
        {
            
        }
    }
}