namespace Telegram.Bot.Commands.Services.Interfaces
{
    public interface IBotClient
    {
        public Task<TelegramBotClient> GetClientAsync();
    }
}
