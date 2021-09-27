using Common;
using System;

namespace Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Subscriber");

            string topic;
            Console.Write("Enter the topic:");
            topic = Console.ReadLine().ToLower();
            var subcriberSocket = new SubscriberSocket(topic);
            subcriberSocket.Connect(Configs.BROKER_IP, Configs.BROKER_PORT);
            Console.WriteLine("Press any key to exit ...");
            Console.ReadLine();
        }
    }
}
