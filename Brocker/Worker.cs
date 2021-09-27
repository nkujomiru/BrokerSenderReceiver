﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Broker
{
    class Worker
    {
        private const int TIME_TOSLEEP = 500;
        public void DoSendMessageWork()
        {
            while (true)
            {
                while(!PayloadStorage.IsEmpty())
                {
                    var payload = PayloadStorage.GetNext();
                    if (payload != null)
                    {
                        var connections = ConnectionsStorage.GetConnectiondByTopic(payload.Topic);
                        foreach (var connection in connections)
                        {
                            var payloadString = JsonConvert.SerializeObject(payload);
                            byte[] data = Encoding.UTF8.GetBytes(payloadString);

                            connection.Socket.Send(data);

                        }
                    }

                }
                Thread.Sleep(500);
            }
        }
    }
}
