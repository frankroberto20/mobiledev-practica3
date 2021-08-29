using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MailApp.Models
{
    public class Mail
    {
        public Mail(string subject, string body, string to, string from, string iconSource, ObservableCollection<string> images)
        {
            Subject = subject;
            Body = body;
            To = to;
            From = from;
            Date = DateTime.Now;
            IconSource = iconSource;
            Images = images;
        }

        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Date { get ; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string IconSource { get; set; }
        public ObservableCollection<string> Images { get; set; }
    }
}
