using System;
using System.Collections.ObjectModel;

namespace Take.Be.Domain
{
    public interface IUser
    {
        System.Net.Sockets.TcpClient TcpClient { get; }
        
        ObservableCollection<Message> Messages { get; }
        
        string NickName { get; }

        DateTime ConnectionTime { get; }

        Message GetLastMessage();

        string ValidateNickName(string nickName);

        string Connect();
    }
}