﻿namespace Take.Be.Server
{
    public interface IServer
    {
        bool Start();

        string ValidateNickName(string nickName);

        string ReceiveMessage(string nickName, string message);

        void Listening();
    }
}
