using System;

namespace Take.Be.Domain
{
    public class Message
    {
        public string Text { get; private set; }

        public DateTime CreationTime { get; private set; }

        public Message(string text)
        {
            Text = text;
            CreationTime = DateTime.Now;
        }
    }
}