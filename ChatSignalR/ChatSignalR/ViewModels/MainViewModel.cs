using ChatSignalR.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChatSignalR.ViewModels
{
    public class MainViewModel : ViewModel
    {
        public string Username { get; set; }
        public ICommand Start => new Command(() =>
        {
            User = Username;

            var chatView = Resolver.Resolve<ChatView>();
            Navigation.PushAsync(chatView);
        });
    }
}
