using System;
using System.Collections.Generic;
using Kev_Net_SocketBuffer;
using Kay_Net_Bindings;

namespace Kay_Net_Client
{
    /* ----------------------------------------
   * |SocketData.cs Class by Kevin Kaymak © 2017|
   * ----------------------------------------
   * This class is needed to handle all info
   * which get send by the server to the client.
   * It checks which packet got send, reads it
   * out and will executed the assigned code for it.
   */
    class SocketData
    {
        //makes sure that you need a packet(byte[]) to read out the dictionary for it.
        private delegate void Packets(byte[] data);
        //dictionary filled with packets to listen to.
        private static Dictionary<int, Packets> _packets;

        public static void InitPackets()
        {
            //creates a new dictionary of packets to listen to so it executes the correct
            //methode when the packet is arriving at the client.
            _packets = new Dictionary<int, Packets>
            {
                //Add your packets in here, so the client knows which methode to execute.
                { (int)ServerPackets.SChatMsg,HandleChatMsg},
            };
        }

        public static void HandleSocketData(byte[] data)
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
            if (_packets.TryGetValue(packet, out Packets _packet))
            {
                //checks which Method is assigned to the packet and executes it,
                //index: we dont need a index since we only get a message from the server.
                //data: the packet byte [] with the information.
                _packet.Invoke(data);
            }
        }

        //Handles the ChatMsg Packet you have assigned at "InitPackets"
        private static void HandleChatMsg(byte[] data)
        {
            //Creates a new instance of the buffer to read out the packet.
            SocketBuffer buffer = new SocketBuffer();
            //writes the packet into a list to make it avaiable to read it out.
            buffer.WriteBytes(data);
            //INFO: You always have to read out the data as you did send it. 
            //In this case you always have to first to read out the packet identifier.
            int packetIdentify = buffer.ReadInteger();
            //In the server side you now send a string as next so you have to read out the string as next.
            string msg = buffer.ReadString();
            //print out the string msg you did send from the server.
            Console.WriteLine("Got Packet Nr '{0}' with message: '{1}'", packetIdentify, msg);
            //we have received a welcome message now send a packet back to server to say thank you!
            SocketSend.SendThankYou();
        }
    }
}