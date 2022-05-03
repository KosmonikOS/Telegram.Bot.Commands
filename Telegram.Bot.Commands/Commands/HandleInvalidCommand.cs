using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Commands.Abstractions;
using Telegram.Bot.Commands.Attributes;
using Telegram.Bot.Commands.Services.Interfaces;
using Telegram.Bot.Types;

namespace Telegram.Bot.Commands.Commands
{
    [InvalidCommand]
    public class HandleInvalidCommand : IBotCommand
    {
        private readonly IBotClient client;

        public HandleInvalidCommand(IBotClient client)
        {
            this.client = client;
        }
        public async Task InvokeAsync(List<string> requestParts,Message message)
        {
            var bot = await client.GetClientAsync();
            await bot.SendTextMessageAsync(message.Chat.Id, "Invalid command , please try again");
        }
    }
}
