using _01.DayOfWeekConsoleClient.ServiceReferenceDayOfWeek;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _01.DayOfWeekConsoleClient
{
    class ConsoleClient
    {
        static void Main()
        {
            ServiceDayOfWeekClient client = new ServiceDayOfWeekClient();
            using (client)
            {
                Console.WriteLine("Today is:");
                Console.WriteLine(client.GetDayOfWeek(DateTime.Now));
                Console.WriteLine("Enter date in format dd.MM.yyyy");
                DateTime date = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", new CultureInfo("bg-BG"));
                Console.WriteLine(client.GetDayOfWeek(date));
            }
        }
    }
}
