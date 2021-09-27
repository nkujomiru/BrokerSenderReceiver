using Common;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Publisher");

            var publisherSocket = new PublisherSocket();
            publisherSocket.Connect(Configs.BROKER_IP, Configs.BROKER_PORT);

            if ( publisherSocket.IsConnected)
            {
                while (true)
                {
                    var payload = readPayload();

                    var data = getBytes(payload);

                    publisherSocket.Send(data);
                }

            }
            Console.ReadLine();
        }

        private static byte[] getBytes<T>(T myObject)
        {
            var JsonString = JsonConvert.SerializeObject(myObject);
            byte[] bytes = Encoding.UTF8.GetBytes(JsonString);
            return bytes;
        }

        private static Payload readPayload()
        {
            var payload = new Payload();
            Console.WriteLine("Enter Topic");
            payload.Topic = Console.ReadLine();
            Console.WriteLine("Enter Message");
            payload.Message = Console.ReadLine();

            return payload;
        }
    }
}
