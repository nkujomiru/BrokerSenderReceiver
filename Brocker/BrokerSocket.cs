using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Broker
{
    public class BrokerSocket
    {
        private Socket _socket;
        private int CONNECTIONS_LIMIT = 8;

        public BrokerSocket()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start(string ip, int port)
        {
            _socket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            _socket.Listen(CONNECTIONS_LIMIT);
            Accept();
        }

        private void Accept()
        {
            _socket.BeginAccept(AcceptedCallback, null);
        }

        private void AcceptedCallback( IAsyncResult result)
        {
            ConnectionInfo connection = new ConnectionInfo();
            try
            {
                connection.Socket = _socket.EndAccept(result);
                connection.Address = connection.Socket.RemoteEndPoint.ToString(); // ip:port
                BeginReceive(connection);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: failed to receive message.\n {ex.Message}");
            }
            finally
            {
                Accept();
            }
        }

        private void receiveCallback( IAsyncResult asyncResult)
        {
            ConnectionInfo connection = asyncResult.AsyncState as ConnectionInfo;
            try
            {
                Socket senderSocket = connection.Socket;
                SocketError response;
                int buffsize = senderSocket.EndReceive(asyncResult, out response);

                if ( response == SocketError.Success)
                {
                    byte[] payload = new byte[buffsize];
                    Array.Copy(connection.Data, payload, payload.Length);

                    PayloadHandler.Handle(payload, connection);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: failed to receive message.\n {ex.Message}");
            }
            finally
            {
                BeginReceive(connection);
            }
        }


        private void BeginReceive(ConnectionInfo connection)
        {
            try
            {
                connection.Socket.BeginReceive(connection.Data, 0, connection.Data.Length,
                    SocketFlags.None, receiveCallback, connection);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                var address = connection.Socket.RemoteEndPoint.ToString();
                connection.Socket.Close();
                Console.WriteLine($"Error: Failed to use {address} socket. Deleting from storage");
                // Del from storage
            }
        }
    }
}
