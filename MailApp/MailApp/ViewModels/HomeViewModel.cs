using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using MailApp.Models;
using MailApp.Views;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MailApp.ViewModels
{
    class HomeViewModel : BaseViewModel
    {
        private Mail _selectedMail;
        public Mail SelectedMail
        {
            get => _selectedMail;
            set
            {
                _selectedMail = value;
                if (_selectedMail != null)
                {
                    SelectedMailCommand.Execute(_selectedMail);
                }
            }
        }
        public bool IsRefreshing { get; set; }
        public ICommand RefreshCommand { get; }
        public ICommand ComposeCommand { get; }
        public ICommand DeleteCommand { get; }
        private ICommand SelectedMailCommand { get; }
        public ObservableCollection<Mail> Mails { get; } = new ObservableCollection<Mail>();

        void OnRefresh()
        {
            IsRefreshing = true;
            Mails.Clear();
            string mailSavedData = Preferences.Get("Mails", null);
            if (mailSavedData != null)
            {
                ObservableCollection<Mail> savedMails = JsonConvert.DeserializeObject<ObservableCollection<Mail>>(mailSavedData);
                foreach (Mail mail in savedMails)
                {
                    Mails.Add(mail);
                }
            }
            IsRefreshing = false;
        }

        async void OnCompose()
        {
            await App.Current.MainPage.Navigation.PushAsync(new AddPage(Mails));
        }

        void OnDelete(Mail mail)
        {
            Mails.Remove(mail);
            Preferences.Set("Mails", JsonConvert.SerializeObject(Mails)); 
        }

        async void OnMailSelected(Mail mail)
        {
            await App.Current.MainPage.Navigation.PushAsync(new MailPage(mail));
        }

        public HomeViewModel()
        {
            RefreshCommand = new Command(OnRefresh);
            ComposeCommand = new Command(OnCompose);
            DeleteCommand = new Command<Mail>(OnDelete);
            SelectedMailCommand = new Command<Mail>(OnMailSelected);

            string mailSavedData = Preferences.Get("Mails", null);
            if (mailSavedData != null)
            {
                Mails = JsonConvert.DeserializeObject<ObservableCollection<Mail>>(mailSavedData);
            }

        }
    }
}
