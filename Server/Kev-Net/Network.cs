using System;
using System.Net.Sockets;
using System.Net;

namespace Kay_Net
{
    /* ----------------------------------------
     * |Networking.cs Class by Kevin Kaymak © 2017|
     * ----------------------------------------
     * This class is needed to create the Server
     * itself. By using Sockets we allow multiple
     * Clients to connect to the Server and listen
     * to the stream to receive data.
     */
    class Network
    {
        //Server Socket Object to create the server itself.
        private static Socket _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //Buffer Object to receive data from a connected client.
        private static byte[] _buffer = new byte[1024];
        //Socket array to allow a max. of connections.
        public static Sockets[] _sockets = new Sockets[Constants.MAX_PLAYERS];

        public static void InitServer()
        {
            //Allow connections from every ip at port '5555'.
            _server.Bind(new IPEndPoint(IPAddress.Any, 5555));
            //Allow 10 connections at the same time, else the 11th have to wait till connection from the 10th was succesfull.
            _server.Listen(10);
            //Accepting the connection and start a asynchronius Callback.
            _server.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private static void AcceptCallback(IAsyncResult ar)
        {
            //Accepts the current connection and binds it to a temporary Socket to allow other connections.
            Socket socket = _server.EndAccept(ar);
            //Accepting a new connection so multiple clients can connect.
            _server.BeginAccept(new AsyncCallback(AcceptCallback), null);
            //Looping through the socket array.
            for (int i = 0; i < Constants.MAX_PLAYERS; i++)
            {
                //Checking if there is an open slot.
                if (_sockets[i].socket == null)
                {
                    //Assign the socket to the open socket slot.
                    _sockets[i].socket = socket;
                    //Assign the index to the socket to see which connection it is.
                    _sockets[i].index = i;
                    //Assign the connections ip
                    _sockets[i].ip = socket.RemoteEndPoint.ToString();
                    //Start the socket to allow incoming data from the connection.
                    _sockets[i].Start();
                    //Print the information on the console.
                    Console.WriteLine("Connection from '{0}' received", _sockets[i].ip);
                    //return the code else the first connection get all open slots!
                    return;
                }
            }
        }

        public static void SendDataTo(int index, byte[] data)
        {
            /*creating a new byte array to set the size of the 
            *outgoing packet, which the client is listening.
            *this method will make sure to not drop packets
            *on the client side, when you send multiple packets
            *at the same time!*/
            byte[] sizeinfo = new byte[4];
            sizeinfo[0] = (byte)data.Length;
            sizeinfo[1] = (byte)(data.Length >> 8);
            sizeinfo[2] = (byte)(data.Length >> 16);
            sizeinfo[3] = (byte)(data.Length >> 24);

            //sending the packet size information first.
            _sockets[index].socket.Send(sizeinfo);
            //send the packet with information itself.
            _sockets[index].socket.Send(data);
        }

    }
}
