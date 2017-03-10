using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace BaggaBot.Modules
{
    public class ReminderModule : IModule
    {
        private InactivityConfiguration inactivityConfiguration = (InactivityConfiguration)ConfigurationManager.GetSection("InactivityTimerSection");
        BaggaBot bot;

        public ReminderModule(BaggaBot bot)
        {
            this.bot = bot;
        }

        public void Load()
        {
            System.Timers.Timer inactiveTimer = new System.Timers.Timer();
            inactiveTimer.Interval = 1000 * inactivityConfiguration.Interval;
            inactiveTimer.Elapsed += delegate { ReminderEvent(bot); };
            inactiveTimer.Enabled = true;
        }

        private async void ReminderEvent(BaggaBot bot)
        {
            foreach (ChannelElement channel in inactivityConfiguration.Channels)
            {
                try
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
                catch (Exception exn)
                {
                    Console.WriteLine($"An exception was thrown: {exn.Message}");
                }

            }
        }
    }
}
