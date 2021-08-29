using MailApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MailApp.ViewModels;

namespace MailApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MailPage : ContentPage
    {
        public MailPage(Mail mail)
        {
            InitializeComponent();
            BindingContext = new MailViewModel(mail);
        }
    }
}