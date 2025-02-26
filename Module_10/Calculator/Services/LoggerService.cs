using Module_10.Calculator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_10.Calculator.Services
{
    internal class LoggerService : ILogger
    {
        public void Error(object sender, string errorMsg)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"ОШИБКА в {sender.GetType().Name}: {errorMsg}");
            Console.ResetColor();
        }

        public void Event(object sender, string eventMsg)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"СОБЫТИЕ в {sender.GetType().Name}: {eventMsg}");
            Console.ResetColor();
        }
    }
}
