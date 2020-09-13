using System;
using System.Net.Sockets;
using System.Text;

namespace Take.Be.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Connect();
            string nickName = Console.ReadLine();
            ValidateNickName(nickName);
        }

        private static bool Connect()
        {
            const int bytesize = 1024 * 1024;
            TcpClient client = new TcpClient("127.0.0.1", 1234);
            NetworkStream stream = client.GetStream();

            try
            {
                var messageBytes = new byte[bytesize];

                stream.Read(messageBytes, 0, messageBytes.Length);
                string serverResponse = Encoding.Unicode.GetString(messageBytes);

                Console.WriteLine(serverResponse.Split('\0')[0]);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                stream.Dispose();
                client.Close();
            }
        }

        private static bool ValidateNickName(string nickName)
        {
            const int bytesize = 1024 * 1024;
            TcpClient client = new TcpClient("127.0.0.1", 1234);
            NetworkStream stream = client.GetStream();

            try
            {
                var messageBytes = new byte[bytesize];

                stream.Write(Encoding.Unicode.GetBytes(nickName), 0, messageBytes.Length);

                stream.Read(messageBytes, 0, messageBytes.Length);

                string serverResponse = Encoding.Unicode.GetString(messageBytes);

                Console.WriteLine(serverResponse.Split('\0')[0]);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                stream.Dispose();
                client.Close();
            }
        }
    }
}
