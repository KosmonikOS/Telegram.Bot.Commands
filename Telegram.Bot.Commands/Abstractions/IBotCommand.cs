using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Telegram.Bot.Commands.Abstractions
{
    public interface IBotCommand
    {
        public Task InvokeAsync(List<string> requestParts,Message message);
    }
}
