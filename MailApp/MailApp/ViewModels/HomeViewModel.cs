﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using MailApp.Models;
using MailApp.Views;
using Xamarin.Forms;

namespace MailApp.ViewModels
{
    class HomeViewModel : BaseViewModel
    {
        private Mail _selectedMail;
        public Mail SelectedMail 
        { 
            get 
            { 
                return _selectedMail;
            }
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
        private ICommand SelectedMailCommand { get; }
        public ObservableCollection<Mail> Mails { get; } = new ObservableCollection<Mail>()
        {
            new Mail("Test1", "Test mail", "frankroberto2000@hotmail.com", "example@mail.com", "UserImage.jpg"),
            new Mail("Test2", "Test mail", "frankroberto2000@hotmail.com", "example@mail.com", "UserImage.jpg")
        };

        void OnRefresh()
        {
            IsRefreshing = true;
            Mails.Clear();
            Mails.Add(new Mail("Test1", "Test mail", "frankroberto2000@hotmail.com", "example@mail.com", "UserImage.jpg"));
            IsRefreshing = false;
        }

        async void OnCompose()
        {
            await App.Current.MainPage.Navigation.PushAsync(new AddPage());
        }

        async void OnMailSelected(Mail mail)
        {
            await App.Current.MainPage.DisplayAlert(mail.Subject, mail.Body, "OK");
        }
        
        public HomeViewModel()
        {
            RefreshCommand = new Command(OnRefresh);
            ComposeCommand = new Command(OnCompose);
            SelectedMailCommand = new Command<Mail>(OnMailSelected);
        }
    }
}