using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Chat.Messages2;
using ChatSignalR.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace ChatSignalR.ViewModels
{
    public class ChatViewModel : ViewModel
    {
        private readonly IChatService chatService;
        public ObservableCollection<Message> Messages { get; private set; }

        private string text;
        public string Text
        {
            get { return text; }
            set => Set(ref text, value);
        }

        public ICommand Send => new Command(async () =>
        {
            var message = new SimpleTextMessage(User)
            {
                Text = this.text
            };

            Messages.Add(new LocalSimpleTextMessage(message));

            await chatService.SendMessage(message);

            Text = string.Empty;
        });

        public ICommand Photo => new Command(async () =>
        {
            var options = new PickMediaOptions();
            options.CompressionQuality = 50;

            var photo = await CrossMedia.Current.PickPhotoAsync();

            UserDialogs.Instance.ShowLoading("Uploading photo");

            var stream = photo.GetStream();
            var bytes = ReadFully(stream);

            var base64photo = Convert.ToBase64String(bytes);

            var message = new PhotoMessage(User)
            {
                Base64Photo = base64photo,
                FileEnding = photo.Path.Split('.').Last()
            };

            Messages.Add(message);
            await chatService.SendMessage(message);

            UserDialogs.Instance.HideLoading();
        });

        public ChatViewModel(IChatService chatService)
        {
            this.chatService = chatService;

            Messages = new ObservableCollection<Message>();

            chatService.NewMessage += ChatService_NewMessage;

            Task.Run(async () => 
            {
                if (!chatService.IsConnected)
                {
                    await chatService.CreateConnection();
                }

                await chatService.SendMessage(new UserConnectedMessage(User));
            });
        }

        private void ChatService_NewMessage(object sender, Events.NewMessageEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (!Messages.Any(x => x.Id == e.Message.Id))
                {
                    Messages.Add(e.Message);
                }
            });
        }

        private byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ms.CopyTo(input);
                return ms.ToArray();
            }
        }
    }
}
