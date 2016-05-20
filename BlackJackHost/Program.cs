using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace BlackJackHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var host = new ServiceHost(typeof(WCFBlackJackService.BlackJackService)))
            {
                host.Open();

                Console.WriteLine("Host is running , press enter to finish process");
                Console.ReadLine();
            }
        }
    }
}
