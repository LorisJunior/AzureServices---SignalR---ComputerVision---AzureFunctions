using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ChatSignalR.ViewModels;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
namespace ChatSignalR.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatView : ContentPage
    {
        private ChatViewModel viewModel;
        public ChatView(ChatViewModel viewModel)
        {
            this.viewModel = viewModel;
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            viewModel.Messages.CollectionChanged += Messages_CollectionChanged;
            BindingContext = viewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeArea = On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();
            MainGrid.HeightRequest = this.Height - safeArea.Top - safeArea.Bottom;
        }

        private void Messages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //MessageList.ScrollTo(viewModel.Messages.Last(), null, ScrollToPosition.End, true);
        }
    }
}