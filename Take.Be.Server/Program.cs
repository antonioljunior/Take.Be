namespace Take.Be.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IServer tcpServer = new TcpServer();
            tcpServer.Start();
            tcpServer.Listening();
            
        }
    }
}