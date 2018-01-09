using System;
using System.Net.Sockets;
using System.Net;

namespace Kay_Net_Client
{
   /* ----------------------------------------
    * |Networking.cs Class by Kevin Kaymak © 2017|
    * ----------------------------------------
    * This class is needed to create the Client
    * itself. By using Sockets we allow this application
    * to connect to the Server and listen
    * to the stream to receive data.
    */
    class Network
    {
        //Client Socket Object to create the client itself.
        private static Socket _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //Buffer Object to receive data from the server.
        private static byte[] _buffer = new byte[1024];

        public static void Connect()
        {
            //Start to connect to the server ('change "127.0.0.1" to your server IP)
            _client.BeginConnect("127.0.0.1", 5555, new AsyncCallback(ConnectCallback), _client);
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            //Stop connecting to server since we are finally conntected!
            _client.EndConnect(ar);
            //starting a loop to receive server network data.
            while (true)
            {
                //calling the method to to receive network data.
                OnReceive();
            }
        }

        private static void OnReceive()
        {
            //creating a byte[] to receive the packet size info.
            byte[] _sizeinfo = new byte[4];
            //creating a byte[] to receive the actual packet.
            byte[] _receivedbuffer = new byte[1024];
            //variables to read out the the packet.
            int totalread = 0, currentread = 0;

            try
            {
                //receiving the packet size information and assign it to a variable.
                currentread = totalread = _client.Receive(_sizeinfo);
                //checking if we are actual receiving something.
                if (totalread <= 0)
                {
                    //if we are not getting any data we are not connected to the server.
                    Console.WriteLine("You are not connected to the server.");
                }
                else
                {
                    //looping through the first packet to receive the first packet size info.
                    while (totalread < _sizeinfo.Length && currentread > 0)
                    {
                        //assigning the total value of the size info.
                        currentread = _client.Receive(_sizeinfo, totalread, _sizeinfo.Length - totalread, SocketFlags.None);
                        totalread += currentread;
                    }

                    //resizing toe info array to receive other data.
                    int messagesize = 0;
                    messagesize |= _sizeinfo[0];
                    messagesize |= (_sizeinfo[1] << 8);
                    messagesize |= (_sizeinfo[2] << 16);
                    messagesize |= (_sizeinfo[3] << 24);
                    //creating an array and assign the packet size info.
                    byte[] data = new byte[messagesize];
                    //the point where we read the out the packet.
                    totalread = 0;
                    //receiving the actual packet and assign it's lengt to a variable.
                    currentread = totalread = _client.Receive(data, totalread, data.Length - totalread, SocketFlags.None);
                    //looping through the whole network message to receive the whole packet to not drop it!
                    while (totalread < messagesize && currentread > 0)
                    {
                        //receiving the actual packet over the network.
                        currentread = _client.Receive(data, totalread, data.Length - totalread, SocketFlags.None);
                        totalread += currentread;
                    }
                    //checking which 'packet' we got.
                    SocketData.HandleSocketData(data);
                }
            }
            catch
            {
                //if we are not getting any data we are not connected to the server.
                Console.WriteLine("You are not connected to the server.");
            }
        }

        public static void SendData(byte[] data)
        {
            //sends the packet to the server.
            _client.Send(data);
            //Print that we send a packet to the server
            Console.WriteLine("Sending a packet to the server!");
        }
    }
}
