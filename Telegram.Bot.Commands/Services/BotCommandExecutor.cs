using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Telegram.Bot.Commands.Attributes;
using Telegram.Bot.Commands.Commands;
using Telegram.Bot.Commands.Services.Interfaces;
using Telegram.Bot.Types;

namespace Telegram.Bot.Commands.Services
{
    public class BotCommandExecutor : IBotCommandExecutor
    {
        private readonly Assembly assembly;
        private readonly IServiceProvider serviceProvider;

        public BotCommandExecutor(Assembly assembly, IServiceProvider serviceProvider)
        {
            this.assembly = assembly;
            this.serviceProvider = serviceProvider;
        }
        public async Task ExecuteCommandAsync(Message message)
        {
            List<string> requestParts = new();
            using (var scope = serviceProvider.CreateScope())
            {
                try
                {
                    requestParts = message.Text.Split(" ").ToList();
                    if (requestParts[0].StartsWith("@"))
                        requestParts = requestParts.Skip(1).ToList();
                    if (requestParts.Count >= 1)
                    {
                        var type = assembly.GetTypes().SingleOrDefault(x =>
                            Attribute.IsDefined(x, typeof(BotCommandAttribute))
                                    && x.GetCustomAttribute<BotCommandAttribute>().Command == requestParts[0]);
                        dynamic command = scope.ServiceProvider.GetService(type);
                        await command.InvokeAsync(requestParts, message);
                        return;
                    }
                }
                catch { }
                await HandleInvalidCommand(scope, requestParts, message);
            }
        }
        private async Task HandleInvalidCommand(IServiceScope scope, List<string> requestParts, Message message)
        {
            var type = assembly.GetTypes().SingleOrDefault(x =>
                        Attribute.IsDefined(x, typeof(InvalidCommandAttribute)));
            if (type == null)
            {
                type = typeof(HandleInvalidCommand);
            }
            dynamic command = scope.ServiceProvider.GetService(type);
            await command.InvokeAsync(requestParts, message);

        }
    }
}
