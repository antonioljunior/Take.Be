using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using Take.Be.Domain.Contracts;

namespace Take.Be.Domain
{
    public class User : Entity<Guid>, IUser
    {
        private readonly IClient client;

        public override Guid Id { get; }

        public string NickName { get; private set; }

        public System.Net.Sockets.TcpClient TcpClient { get; private set; }

        public ObservableCollection<Message> Messages { get; private set; }

        public DateTime ConnectionTime { get; private set; }

        public User(IClient client)
        {
            this.client = client;
            Messages = new ObservableCollection<Message>();
        }

        public User(Guid id, System.Net.Sockets.TcpClient tcpClient, string nickName)
        {
            Id = id;
            TcpClient = tcpClient;
            NickName = nickName;
            ConnectionTime = DateTime.Now;
        }

        public string ValidateNickName(string nickName)
        {
            try
            {
                if (string.IsNullOrEmpty(nickName))
                    return SystemMessages.NICKNAME_EMPTY_ERROR;

                return client.SendMessage(string.Concat(SystemMessages.NICKNAME_VALIDATE, nickName));
            }
            catch (Exception ex)
            {
                //TODO: Implement log mechanism 
                return ex.Message;
            }
        }

        public string Connect()
        {
            try
            {
                return client.Connect();
            }
            catch (Exception ex)
            {
                //TODO: Implement log mechanism 
                return ex.Message;
            }
        }

        public Message GetLastMessage()
        {
            return Messages.LastOrDefault();
        }
    }
}
