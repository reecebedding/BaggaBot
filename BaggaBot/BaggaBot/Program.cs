using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace BaggaBot
{
    class Program
    {
        private static InactivityConfiguration configuration = (InactivityConfiguration)ConfigurationManager.GetSection("InactivityTimerSection");

        static void Main(string[] args)
        {
            BaggaBot bot = new BaggaBot();
            
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Interval = 1000 * configuration.Interval;
            aTimer.Elapsed += delegate { ReminderEvent(bot); };
            aTimer.Enabled = true;

            bot.Start();

            Console.Read();
        }

        private static async void ReminderEvent(BaggaBot bot)
        {
            foreach (ChannelElement channel in configuration.Channels)
            {   
                var lastMessage = await bot.GetLastMessageInChannel(channel.Id);

                if (lastMessage != null)
                {
                    if ((DateTime.Now - lastMessage.Timestamp).TotalHours > 1)
                    {
                        bot.say(channel.Id, configuration.DisplayMessage);
                    }
                }
            }
        }

    }
}