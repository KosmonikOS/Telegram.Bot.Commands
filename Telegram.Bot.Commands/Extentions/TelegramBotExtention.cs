using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Commands.Services.Interfaces;

namespace Telegram.Bot.Commands.Extentions
{
    public static class TelegramBot
    {
        public async static void UseTelegramBot(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var botService = scope.ServiceProvider.GetService<IBotClient>();
                botService.GetClientAsync().Wait();
            }
        }
    }
}
