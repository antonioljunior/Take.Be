using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Take.Be.Domain
{
    public class TcpServer : IServer
    {
        private readonly IPEndPoint iPEndPoint;
        private readonly TcpListener tcpListener;
        private readonly IList<User> users;
        private bool IsRunning;

        public TcpServer()
        {
            iPEndPoint = new IPEndPoint(IPAddress.Loopback, 5000);
            tcpListener = new TcpListener(iPEndPoint);
            users = new List<User>();
        }

        public bool TryStart()
        {
            try
            {
                tcpListener.Start();

                IsRunning = true;

                var thread = new Thread(Listening);

                thread.Start();

                return true;
            }
            catch (Exception ex)
            {
                //Should Log the error but return false to avoid break the application
                return false;
            }
        }

        public bool TryStop()
        {
            try
            {
                users.Clear();
                tcpListener.Stop();
                IsRunning = false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Listening()
        {
            while (IsRunning)
            {
                System.Net.Sockets.TcpClient tcpClient = tcpListener.AcceptTcpClient();

                var receiveMessageThread = new Thread(new ParameterizedThreadStart(ReceiveMessage));
                receiveMessageThread.Start(tcpClient);
            }
        }

        public bool ConnectUser(System.Net.Sockets.TcpClient client, string nickName)
        {
            try
            {
                var user = new User(Guid.NewGuid(), client, nickName);
                users.Add(user);
                
                return true;
            }
            catch (Exception ex)
            {
                //TODO: Implement log mechanism 
                return false;
            }
        }

        public bool DisconnectUser(string nickName)
        {
            var userToDisconnect = users.Where(u => u.NickName == nickName).FirstOrDefault();
            if (userToDisconnect != null)
            {
                users.Remove(userToDisconnect);
                return true;
            }

            return false;
        }

        public void SendPublicMessage(User userFrom, User userTo)
        {
            throw new NotImplementedException();
        }

        public void SendBroadCastMessage(object message)
        {
            StreamWriter streamWriter;
            foreach (User user in users)
            {
                streamWriter = new StreamWriter(user.TcpClient.GetStream());
                WriteMessageToSender(streamWriter, message.ToString());
                streamWriter = null;
            }
        }

        public void ReceiveMessage(object tcpClient)
        {
            var client = (System.Net.Sockets.TcpClient)tcpClient;
            var streamWriter = new StreamWriter(client.GetStream());
            var streamReader = new StreamReader(client.GetStream());
            string clientMessage = streamReader.ReadLine();
            string roomName = "#general";

            if (clientMessage == SystemMessages.FIRST_CONNECTION)
                WriteMessageToSender(streamWriter, SystemMessages.WELCOME_TO_CHAT);

            if (clientMessage.Contains(SystemMessages.NICKNAME_VALIDATE))
            {
                //TODO: Create a message pattern based on JSON to avoid this kind of decomposition
                string nickName = clientMessage.Split(':')[1];
                if (NickNameAlreadyTaken(nickName))
                    WriteMessageToSender(streamWriter, string.Format(SystemMessages.NICKNAME_ALREADY_TAKEN, nickName));
                else
                {
                    ConnectUser(client, nickName);
                    WriteMessageToSender(streamWriter, string.Format(SystemMessages.NICKNAME_REGISTERED_SUCCESS, nickName, roomName));
                    var thread = new Thread(new ParameterizedThreadStart(SendBroadCastMessage));
                    thread.Start(string.Format("\"{0}\" has joined {1}", nickName, roomName));
                }
            }
        }

        private bool NickNameAlreadyTaken(string nickName)
        {
            return users.Where(u => u.NickName == nickName).Any();
        }

        private void WriteMessageToSender(StreamWriter streamWriter, string message)
        {
            streamWriter.WriteLine(message);
            streamWriter.Flush();
        }

        public string ValidateNickName(string nickName)
        {
            if (users.Any(u => u.NickName == nickName))
                return string.Format(SystemMessages.NICKNAME_ALREADY_TAKEN, nickName);

            return string.Empty;
        }
    }
}