namespace Telegram.Bot.Commands.Services.Interfaces
{
    public class BotClient :IBotClient
    {
        public BotClient(string token, string webhook)
        {
            this.token = token;
            this.webhook = webhook;
        }
        private TelegramBotClient client;
        private readonly string token;
        private readonly string webhook;

        public async Task<TelegramBotClient> GetClientAsync()
        {
            if (client == null)
            {
                client = new TelegramBotClient(token);
                await client.SetWebhookAsync(webhook);
            }
            return client;
        }
    }
}

