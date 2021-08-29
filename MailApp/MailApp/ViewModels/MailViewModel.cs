using MailApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailApp.ViewModels
{
    class MailViewModel : BaseViewModel
    {
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
