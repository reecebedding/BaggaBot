using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaggaBot
{
    class Program
    {
        private static List<KeyValuePair<string, ulong>> channels = new List<KeyValuePair<string, ulong>>()
        {
            new KeyValuePair<string, ulong>("channel_name", 123456789012345678)
        };
           
        static void Main(string[] args)
        {
            BaggaBot bot = new BaggaBot();
            
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Interval = 1000 * 10;
            aTimer.Elapsed += delegate { ReminderEvent(bot); };
            aTimer.Enabled = true;

            bot.Start();
        }

        private static async void ReminderEvent(BaggaBot bot)
        {
            ulong channelId = channels.Find(x => x.Key == "eye_bleach").Value;
            var lastMessage = await bot.GetLastMessageInChannel(channelId);
            
            if (lastMessage != null)
            {
                if ((DateTime.Now - lastMessage.Timestamp).TotalHours > 24)
                {
                    bot.say(channelId, "@here Its been 24 hours since a post .. c'mon all!");
                }
            }
        }

    }
}