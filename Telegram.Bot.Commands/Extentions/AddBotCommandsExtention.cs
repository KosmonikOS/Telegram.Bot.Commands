using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Telegram.Bot.Commands.Abstractions;
using Telegram.Bot.Commands.Attributes;
using Telegram.Bot.Commands.Services;
using Telegram.Bot.Commands.Services.Interfaces;

namespace Telegram.Bot.Commands.Extentions
{
    public static class AddBotCommandsExtention
    {
        public static IServiceCollection AddBotCommands(this IServiceCollection services,Assembly assembly,string token,string webhook)
        {
            if(assembly == null)
            {
                throw new ArgumentNullException("Assembly is required to register bot commands");
            }
            RegisterBotCommands(services,assembly);
            RegisterInternalCommands(services);
            services.AddScoped<IBotClient>(provider => new BotClient(token, webhook));
            services.AddScoped<IBotCommandExecutor>(provider => new BotCommandExecutor(assembly,provider));
            return services;
        }
        private static void RegisterBotCommands(IServiceCollection services,Assembly assembly)
        {
            var types = assembly.GetTypes().Where(x => typeof(IBotCommand).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);
            foreach(var type in types)
            {
                services.AddTransient(type);
            }
        }
        private static void RegisterInternalCommands(IServiceCollection services)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => typeof(IBotCommand).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);
            foreach (var type in types)
            {
                services.AddTransient(type);
            }
        }
    }
}
