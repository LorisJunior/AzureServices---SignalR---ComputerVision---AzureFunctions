using Chat.Messages2;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatSignalR.Events
{
    public class NewMessageEventArgs : EventArgs
    {
        public Message Message { get; set; }
        public NewMessageEventArgs() {}
        public NewMessageEventArgs(Message message)
        {
            Message = message;
        }
    }
}
