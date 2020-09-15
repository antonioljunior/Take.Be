using System;
using System.IO;
using System.Net;
using Take.Be.Domain.Contracts;

namespace Take.Be.Domain
{
    public class TcpClient : IClient
    {
        public string Connect()
        {
            try
            {
                System.Net.Sockets.TcpClient client = TcpClientFactory();

                SendMessage(client, SystemMessages.FIRST_CONNECTION);

                return MessageSentResponse(client);
            }
            catch (Exception ex)
            {
                //TODO: Implement log mechanism 
                return ex.Message;
            }
        }

        private System.Net.Sockets.TcpClient TcpClientFactory()
        {
            var client = new System.Net.Sockets.TcpClient();
            client.Connect(IPAddress.Loopback, 5000);
            return client;
        }

        public string SendMessage(string message)
        {
            try
            {
                System.Net.Sockets.TcpClient client = TcpClientFactory();

                SendMessage(client, message);

                return MessageSentResponse(client);
            }
            catch (Exception ex)
            {
                //TODO: Implement log mechanism
                return ex.Message;
            }
        }

        private string MessageSentResponse(object tcpClient)
        {
            var streamReader = new StreamReader(((System.Net.Sockets.TcpClient)tcpClient).GetStream());
            return streamReader.ReadLine();
        }

        public bool Disconnect()
        {
            return false;
        }

        public bool SendMessage(System.Net.Sockets.TcpClient client, string message)
        {
            try
            {
                var streamWriter = new StreamWriter(client.GetStream());
                streamWriter.WriteLine(message);
                streamWriter.Flush();
                return true;
            }
            catch (Exception ex)
            {
                //TODO: Implement log mechanism 
                return false;
            }
        }
    }
}
