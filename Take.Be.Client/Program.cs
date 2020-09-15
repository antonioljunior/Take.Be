using System;
using Take.Be.Domain;

namespace Take.Be.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            IUser user = new User(new TcpClient());

            Console.WriteLine(user.Connect());
            
            string nickName = Console.ReadLine();
            string validNickName = user.ValidateNickName(nickName);

            while (validNickName == SystemMessages.NICKNAME_EMPTY_ERROR)
                nickName = ReadNickName(user, ref validNickName);

            while (validNickName == string.Format(SystemMessages.NICKNAME_ALREADY_TAKEN, nickName))
                nickName = ReadNickName(user, ref validNickName);

            Console.WriteLine(validNickName);

            Console.ReadLine();
        }

        private static string ReadNickName(IUser user, ref string validNickName)
        {
            string nickName;
            Console.WriteLine(validNickName);
            nickName = Console.ReadLine();
            validNickName = user.ValidateNickName(nickName);
            return nickName;
        }
    }
}
