using Common;
using System;

namespace Broker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Broker");

            BrokerSocket socket = new BrokerSocket();
            socket.Start(Configs.BROKER_IP, Configs.BROKER_PORT);
            Console.ReadLine();

        }
    }
}
