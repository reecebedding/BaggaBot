using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace BaggaBot
{
    static class BaggaBotCommands
    {
        public static async Task HelpCommand(CommandEventArgs e)
        {
            string message = "Usage: \n\n ping - are you alive?";
            await e.Channel.SendMessage(message);
        }

        public static async Task Ping(CommandEventArgs e)
        {
            await e.Channel.SendMessage("Pong");
        }
    }
}
