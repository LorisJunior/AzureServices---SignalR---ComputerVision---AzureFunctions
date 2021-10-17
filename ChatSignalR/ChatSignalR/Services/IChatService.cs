using Chat.Messages2;
using ChatSignalR.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatSignalR.Services
{
    public interface IChatService
    {
        event EventHandler<NewMessageEventArgs> NewMessage;
        bool IsConnected { get; }
        Task CreateConnection();
        Task SendMessage(Message message);
        Task Dispose();
    }
}
