namespace Telegram.Bot.Commands.Attributes
{
    public class BotCommandAttribute : Attribute
    {
        public BotCommandAttribute(string command)
        {
            Command = command;
        }
        public string Command { get; }
    }
}
