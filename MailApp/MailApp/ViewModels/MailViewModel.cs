using MailApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MailApp.ViewModels
{
    class MailViewModel : BaseViewModel
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public string IconSource { get; set; }
        public ObservableCollection<string> Images { get; set; } = new ObservableCollection<string>();
        public bool IsCollectionEmpty { get; set; } = false;
        public MailViewModel(Mail mail)
        {
            To = $"to {mail.To}";
            From = mail.From;
            Subject = mail.Subject;
            Body = mail.Body;
            Date = mail.Date;
            IconSource = mail.IconSource;
            Images = mail.Images;

            if (Images.Count == 0)
            {
                IsCollectionEmpty = false;
            }
            else
            {
                IsCollectionEmpty = true;
            }
        }
    }
}
