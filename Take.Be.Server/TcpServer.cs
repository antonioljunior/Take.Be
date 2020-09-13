using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Take.Be.Server
{
    public class TcpServer : IServer
    {
        private const int BYTE_SIZE = 1024 * 1024;
        private const int PORT_NUMBER = 1234;
        private readonly IPEndPoint ep;
        private readonly TcpListener listener;
        private readonly object locker;
        private readonly Dictionary<string, TcpClient> clients;

        public TcpServer()
        {
            ep = new IPEndPoint(IPAddress.Loopback, PORT_NUMBER);
            listener = new TcpListener(ep);
            clients = new Dictionary<string, TcpClient>();
            locker = new object();
        }

        public bool Start()
        {
            try
            {
                listener.Start();
                Console.WriteLine("The Server has been started successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occured when Starting the Server:", ex.Message);
                return false;
            }
        }

        public void Listening()
        {
            while (true)
            {
                var buffer = new byte[BYTE_SIZE];

                TcpClient client = listener.AcceptTcpClient();
                client.GetStream().Read(buffer, 0, BYTE_SIZE);

                string nickName = Encoding.Unicode.GetString(buffer);

                lock (locker) clients.Add(nickName, client);

                Thread t = new Thread(handle_clients);
                t.Start(nickName);



                byte[] bytes = Encoding.Unicode.GetBytes("*** Welcome to our chat server. Please provide a nickname: ");
                client.GetStream().Write(bytes, 0, bytes.Length); // Send the response  
            }
        }

        public void handle_clients(object nickName)
        {
            TcpClient client;

            lock (locker) client = clients[nickName.ToString()];

            while (true)
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int byte_count = stream.Read(buffer, 0, buffer.Length);

                if (byte_count == 0)
                {
                    break;
                }

                string data = Encoding.ASCII.GetString(buffer, 0, byte_count);
                broadcast(data);
                Console.WriteLine(data);
            }

            lock (locker) clients.Remove(nickName.ToString());
            client.Client.Shutdown(SocketShutdown.Both);
            client.Close();
        }

        public void broadcast(string data)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data + Environment.NewLine);

            lock (locker)
            {
                foreach (TcpClient c in clients.Values)
                {
                    NetworkStream stream = c.GetStream();
                    stream.Write(buffer, 0, buffer.Length);
                }
            }
        }

        public string ReceiveMessage(string nickName, string message)
        {
            throw new NotImplementedException();
        }

        public string ValidateNickName(string nickName)
        {
            throw new NotImplementedException();
        }
    }
}
