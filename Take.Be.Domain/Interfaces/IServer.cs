namespace Take.Be.Domain
{
    public interface IServer
    {
        bool TryStart();

        bool TryStop();

        string ValidateNickName(string nickName);

        void ReceiveMessage(object tcpClient);
    }
}
