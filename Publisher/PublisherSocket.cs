using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Publisher
{
    class PublisherSocket
    {
        private Socket _socket { get; set; }
        public bool IsConnected { get; set; }

        public PublisherSocket()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect(string ipAddress, int port)
        {
            _socket.BeginConnect(new IPEndPoint(IPAddress.Parse(ipAddress), port), ConnectedCallback, null);
            // TODO could imporve
            Thread.Sleep(2000);
        }

        private void ConnectedCallback(IAsyncResult ar)
        {
            if (_socket.Connected)
            {
                Console.WriteLine("Sender is connected to Brocker");
            }
            else
            {
                Console.WriteLine("Error: Sender is not connected to Brocker");
            }
            IsConnected = _socket.Connected;
        }

        public void Send (byte[] data)
        {
            try
            {
                _socket.Send(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending data, message:\n{ex}");
            }
        }
    }
}
