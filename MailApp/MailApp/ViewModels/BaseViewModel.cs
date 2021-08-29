using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MailApp.ViewModels
{
    class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
