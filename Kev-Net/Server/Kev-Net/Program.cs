using System;
using Kay_Net_Bindings;

namespace Kay_Net
{
    class Program
    {
        static void Main(string[] args)
        {
            //Populate the socket array to fill it with infomration.
            InitSockets();
            //Initialize the packet list to listen to.
            SocketData.InitPackets();
            //Initialize and start the server.
            Network.InitServer();
            //Print out that the server succesfully has started.
            Console.WriteLine("Server has succesfully started and is ready to accept {0} connections.", Constants.MAX_PLAYERS);
            //Keeps the console open and running.
            Console.ReadLine();
        }

        static void InitSockets()
        {
            //Initialize the Sockets.
            for (int i = 0; i < Constants.MAX_PLAYERS; i++)
            {
                Network._sockets[i] = new Sockets();
            }
        }
    }
}
