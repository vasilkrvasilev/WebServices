using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02.ServiceContainsConsoleClient
{
    class ConsoleClient
    {
        static void Main()
        {
            ServiceReferenceContains.ServiceContainsClient client = new ServiceReferenceContains.ServiceContainsClient();
            using (client)
            {
                Console.WriteLine("Enter word");
                string word = Console.ReadLine();
                Console.WriteLine("Enter searched in text");
                string text = Console.ReadLine();
                Console.WriteLine(client.GetContainNumber(word, text));
            }
        }
    }
}
