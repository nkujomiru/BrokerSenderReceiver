﻿using Common;
using System;
using System.Threading.Tasks;

namespace Broker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Broker");

            BrokerSocket socket = new BrokerSocket();
            socket.Start(Configs.BROKER_IP, Configs.BROKER_PORT);
            var worker = new Worker();
            Task.Factory.StartNew(worker.DoSendMessageWork, TaskCreationOptions.LongRunning);
            Console.ReadLine();

        }
    }
}
