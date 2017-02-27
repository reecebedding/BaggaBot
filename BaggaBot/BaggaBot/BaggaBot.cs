using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace BaggaBot
{
    class BaggaBot
    {
        DiscordClient discord;

        public BaggaBot()
        {
            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            discord.UsingCommands(x => {
                x.PrefixChar = '!';
                x.AllowMentionPrefix = true;
            });

            var commands = discord.GetService<CommandService>();
            commands.CreateCommand("test")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("works");
                });

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("", TokenType.Bot);
            });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
