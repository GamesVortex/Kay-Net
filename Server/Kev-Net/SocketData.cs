using System.Collections.Generic;
using Kev_Net_SocketBuffer;

namespace Kay_Net
{
    /* ----------------------------------------
    * |SocketData.cs Class by Kevin Kaymak © 2017|
    * ----------------------------------------
    * This class is needed to handle all info
    * which get send by the client to the server.
    * It checks which packet got send, reads it
    * out and will executed the assigned code for it.
    */
    class SocketData
    {
        //makes sure that you need a index and packet(byte[]) to read out the dictionary for it.
        private delegate void Packets(int index, byte[] data);
        //dictionary filled with packets to listen to.
        private static Dictionary<int, Packets> _packets;

        public static void InitPackets()
        {
            //creates a new dictionary of packets to listen to so it executes the correct
            //methode when the packet is arriving at the server.
            _packets = new Dictionary<int, Packets>
            {
                //Add your packets in here, so the server knows which methode to execute.
            };
        }

        public static void HandleSocketData(int index, byte[]data)
        {
            //creating a new instance of 'SocketBuffer' to read out the packet.
            SocketBuffer buffer = new SocketBuffer();
            //writing incoming packet to the buffer.
            buffer.WriteBytes(data);
            //reads out the packet to see which packet we got.
            int packet = buffer.ReadInteger();
            //closes the buffer.
            buffer.Dispose();
            //checking if we are listening to that packet in the _packets Dictionary.
            if(_packets.TryGetValue(packet,out Packets _packet))
            {
                //checks which Method is assigned to the packet and executes it,
                //index: the socket which sends the data
                //data: the packet byte [] with the information.
                _packet.Invoke(index, data);
            }
        }

    }
}
