using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broker
{
    static class ConnectionsStorage
    {
        private static List<ConnectionInfo> _connections;
        private static object _loker;

            static ConnectionsStorage()
        {
            _connections = new List<ConnectionInfo>();
            _loker = new object();
        }
        public static void Add (ConnectionInfo connection)
        {
            lock(_loker)
            {
                _connections.Add(connection);
            }
        }
         public static void Remove (string address)
        {
            lock(_loker)
            {
                _connections.RemoveAll(x => x.Address == address);
            }
        }

        public static List<ConnectionInfo>GetConnectiondByTopic(string topic)
        {
            List<ConnectionInfo> selectedConnections;
            lock(_loker)
            {
                selectedConnections = _connections.Where(x => x.Topic == topic).ToList();
            }
            return selectedConnections;
        }
    }
}
