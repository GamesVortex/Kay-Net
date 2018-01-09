using System;
using System.Net.Sockets;
using System.Net;

namespace Kay_Net
{
    /* ----------------------------------------
    * |Sockets.cs Class by Kevin Kaymak © 2017|
    * ----------------------------------------
    * This class is needed to allow multiple
    * Sockets/Clients/Player to connect to the
    * Server.
    */
    class Sockets
    {
        //Assign a index to this connection.
        public int index;
        //Assign the connections ip address.
        public string ip;
        //Assign the connections Socket.
        public Socket socket;
        //Checks if the player is disconnecting.
        public bool closing = false;
        //Assign connections incoming data(Packet).
        private byte[] buffer = new byte[1024];

        public void Start()
        {
            //Start listening to connections stream, to allow sending data over the network.
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            //Connection is not closing.
            closing = false;

            //We are sending a welcome message to the client from the server!
            SocketSend.SendWelcomeMsg(index);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            //Gets the state of the connection to receive new incoming data.
            Socket socket = (Socket)ar.AsyncState;
            try
            {
                //Gets the length of the data packet from connection.
                int received = socket.EndReceive(ar);
                //If we are receiving nothing then the connection is closed.
                if (received <= 0)
                {
                    //Properly closing the connection from the server.
                    CloseSocket();
                }
                //If we are receiving data and the connection still remains.
                else
                {
                    //resizing the byte array with the length of the received data.
                    byte[] databuffer = new byte[received];
                    //copying the packet information with the received length to a new array 'databuffer'.
                    Array.Copy(buffer, databuffer, received);
                    //checking which 'packet' we got.
                    SocketData.HandleSocketData(index, databuffer);
                    //Start listening to connections stream, to allow getting other data over the network.
                    socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
                }
            }
            catch
            {
                //Properly closing the connection from the server.
                CloseSocket();
            }
        }

        private void CloseSocket()
        {
            //we are closing the connection from the server.
            closing = true;
            //print the information in the console which connection got closed.
            Console.WriteLine("Connection from {0} has ben terminated.", ip);
            //Close the socket properly from the server.
            socket.Close();
            //set the socket to 'null' to let other players connect on this socket.
            socket = null;
        }
    }
}
