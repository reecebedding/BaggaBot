using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using BaggaBot.Modules;

namespace BaggaBot
{
    class Program
    {
        static void Main(string[] args)
        {   
            BaggaBot bot = new BaggaBot();

            LoadModules(bot);
            
            bot.Start();
            Console.Read();
        }

        static void LoadModules(BaggaBot bot)
        {
            new ReminderModule(bot).Load();
        }
    }
}