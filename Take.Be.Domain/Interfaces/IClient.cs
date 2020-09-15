namespace Take.Be.Domain.Contracts
{
    public interface IClient
    {
        string Connect();

        bool Disconnect();

        string SendMessage(string message);
    }
}
