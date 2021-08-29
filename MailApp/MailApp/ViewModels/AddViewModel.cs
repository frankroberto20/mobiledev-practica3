using MailApp.Models;
using MailApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MailApp.ViewModels
{
    class AddViewModel : BaseViewModel
    {
        public AddViewModel(ObservableCollection<Mail> mails)
        {
            SendCommand = new Command(async () => {
                mails.Add(new Mail(Subject, Body, To, From, "UserIcon.png", Images));
                await App.Current.MainPage.Navigation.PopAsync();
            });
            PickImageCommand = new Command(OnPickImage);
        }
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
