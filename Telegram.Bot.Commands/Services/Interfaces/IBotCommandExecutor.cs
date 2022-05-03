using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Telegram.Bot.Commands.Services.Interfaces
{
    public interface IBotCommandExecutor
    {
        public Task ExecuteCommandAsync(Message message);
    }
}
