using System;
using Take.Be.Domain;

namespace Take.Be.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IServer tcpServer = new TcpServer();
            if (tcpServer.TryStart())
                Console.WriteLine(SystemMessages.SERVER_STARTED);
        }
    }
}