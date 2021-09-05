using MailApp.Models;
using MailApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Plugin.LocalNotifications;

namespace MailApp.ViewModels
{
    class AddViewModel : BaseViewModel
    {
        public AddViewModel(ObservableCollection<Mail> mails)
        {
            SendCommand = new Command(async () => {
                mails.Add(new Mail(Subject, Body, To, From, "UserIcon.png", Images));
                Preferences.Set("Mails", JsonConvert.SerializeObject(mails)); 
                await App.Current.MainPage.Navigation.PopAsync();
                try
                {
                    EmailMessage message = new EmailMessage
                    {
                        Subject = Subject,
                        Body = Body
                    };
                    foreach (string image in Images)
                    {
                        message.Attachments.Add(new EmailAttachment(image));
                    }
                    await Email.ComposeAsync(message);
                    CrossLocalNotifications.Current.Show("Mail notification", "Mail sent successfully", 0, DateTime.Now.AddSeconds(5));
                }
                catch
                {
                    Console.WriteLine("Error sending mail.");
                }
            });
            PickImageCommand = new Command(OnPickImage);
        }
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public ObservableCollection<string> Images { get; set; } = new ObservableCollection<string>();
        public bool IsCollectionEmpty { get; set; } = false;
        public string PhotoPath { get; set; }
        public ICommand SendCommand { get; }
        public ICommand PickImageCommand { get; }

        async void OnPickImage()
        {
            await PickPhotoAsync();
            if (Images.Count == 0)
            {
                IsCollectionEmpty = false;
            }
            else
            {
                IsCollectionEmpty = true;
            }
        }

        async Task PickPhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($"PickPhotoAsync COMPLETED: {PhotoPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PickPhotoAsync THREW: {ex.Message}");
            }
        }

        async Task LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                PhotoPath = null;
                return;
            }
            // save the file into local storage
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            PhotoPath = newFile;
            Images.Add(PhotoPath);
        }
    }
}
