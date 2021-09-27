﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broker
{
    public class PayloadHandler
    {
        public static void Handle (byte[] payloadBytes, ConnectionInfo connection)
        {
            var payloadString = Encoding.UTF8.GetString(payloadBytes);
            Console.WriteLine(payloadString);
        }
    }
}
