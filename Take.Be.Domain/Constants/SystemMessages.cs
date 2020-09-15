namespace Take.Be.Domain
{
    public class SystemMessages
    {
        public const string CONNECTION_ERROR = "Sorry but, some error has occurred when trying to connect to the server.";

        public const string NICKNAME_EMPTY_ERROR = "Please inform a not empty nick name.";

        public const string FIRST_CONNECTION = "{first_connection}";

        public const string NICKNAME_VALIDATE = "{nickname}:";

        public const string WELCOME_TO_CHAT = "Welcome to our chat server. Please provide a nickname: ";

        public const string NICKNAME_ALREADY_TAKEN = "Sorry the nickname {0} is already taken. Please choose a different one.";

        public const string NICKNAME_REGISTERED_SUCCESS = "You are registered as {0}. Joining {1}.";

        public const string SERVER_STARTED = "Take.Be.Server has been started successfully.";
    }
}
