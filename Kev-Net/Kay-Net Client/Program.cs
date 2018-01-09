using System;

namespace Kay_Net_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize the packet list to listen to.
            SocketData.InitPackets();
            //Connect to the Server!
            Network.Connect();
            //Print out that we succsefully connected to the server.
            Console.WriteLine("You are now connected to the server!");
            //Keeps the console open and running.
            Console.ReadLine();
        }
    }
}
