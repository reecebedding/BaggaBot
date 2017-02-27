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
        private static InactivityConfiguration inactivityConfiguration = (InactivityConfiguration)ConfigurationManager.GetSection("InactivityTimerSection");

        static void Main(string[] args)
        {
            BaggaBot bot = new BaggaBot();
            
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Interval = 1000 * inactivityConfiguration.Interval;
            aTimer.Elapsed += delegate { ReminderEvent(bot); };
            aTimer.Enabled = true;

            bot.Start();

            Console.Read();
        }

        private static async void ReminderEvent(BaggaBot bot)
        {
            foreach (ChannelElement channel in inactivityConfiguration.Channels)
            {   
                var lastMessage = await bot.GetLastMessageInChannel(channel.Id);

                if (lastMessage != null)
                {
                    if ((DateTime.Now - lastMessage.Timestamp).TotalMinutes > inactivityConfiguration.InactivityPeriod)
                    {
                        bot.say(channel.Id, inactivityConfiguration.DisplayMessage);
                    }
                }
            }
        }

    }
}