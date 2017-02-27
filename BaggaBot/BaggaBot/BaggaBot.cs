using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using Discord;
using Discord.Commands;

namespace BaggaBot
{
    class BaggaBot
    {
        DiscordClient discord;
        CommandService commands;

        public BaggaBot()
        {
            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            discord.UsingCommands(x =>
            {
                x.PrefixChar = '!';
                x.AllowMentionPrefix = true;
            });

            commands = discord.GetService<CommandService>();
        }

        public void say(ulong channelId, string message)
        {
            if (discord != null && discord.State == ConnectionState.Connected)
            {
                var channel = discord.GetChannel(channelId);
                channel.SendMessage(message);
            }
        }

        public async Task<Message> GetLastMessageInChannel(ulong channelId)
        {
            if (discord != null && discord.State == ConnectionState.Connected)
            {
                var channel = discord.GetChannel(channelId);
                await channel.DownloadMessages();
                Console.WriteLine($"Downloaded messages for channel: {channel.Name}");

                var messages = channel.Messages;

                if (messages.Count() > 0)
                {
                    return messages.OrderBy(x => x.Timestamp).Last();
                }
            }
            return null;
        }

        public void Start()
        {
            string token = ConfigurationManager.AppSettings.Get("bot-token");
            discord.Connect(token, TokenType.Bot);
        }



        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
