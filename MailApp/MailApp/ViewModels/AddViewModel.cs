using MailApp.Models;
using MailApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MailApp.ViewModels
{
    class AddViewModel : BaseViewModel
    {
        public AddViewModel(ObservableCollection<Mail> mails)
        {
            SendCommand = new Command(async () => {
                mails.Add(new Mail(Subject, Body, To, From, "UserIcon.png"));
                await App.Current.MainPage.Navigation.PopAsync();
            });
        }

        public ICommand SendCommand { get; }
    }
}
