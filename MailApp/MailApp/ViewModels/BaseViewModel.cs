using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace MailApp.ViewModels
{
    class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public string IconSource { get; set; }
        public ObservableCollection<string> Images { get; set; } = new ObservableCollection<string>();
        public bool IsCollectionEmpty { get; set; } = false;
    }
}
