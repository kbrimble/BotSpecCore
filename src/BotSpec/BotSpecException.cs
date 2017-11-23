using System;

namespace BotSpec
{
    public class BotSpecException : Exception
    {
        public BotSpecException() { }
        public BotSpecException(string message) : base(message) { }
        public BotSpecException(string message, Exception inner) : base(message, inner) { }
    }
}