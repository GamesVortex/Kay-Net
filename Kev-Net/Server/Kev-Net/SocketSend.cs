//using the shared project to access shared classes.
using Kay_Net_Bindings;
using Kev_Net_SocketBuffer;

namespace Kay_Net
{
    /* ----------------------------------------
     * |SocketSend.cs Class by Kevin Kaymak © 2017|
     * ----------------------------------------
     * This class is needed to send packets from the server
     * to the client over the network!
     */
    class SocketSend
    {
        //Sample Send Method. You do need a index and to send
        //data to the correct connected client(int index).
        //If you do want send a packet over the network from the server
        //you always have to design a sending method like the sample one.
        //Optinal: (string chatMsg to assign a written text to send over network)
        public static void SendWelcomeMsg(int index)
        {
            //create a new buffer to write data in it!
            SocketBuffer buffer = new SocketBuffer();
            //Assign the identifier to the packet! Never forget this
            //else the client does not now what to do with the received packet.
            buffer.WriteInteger((int)ServerPackets.SChatMsg);
            //add your chatMsg to the packet, so the client can read it out.
            buffer.WriteString("Welcome to the server. You are now conencted with me!");
            //send data over the network. to a specific client. convert the buffer to a byte[] to make it available to send over the network.
            Network.SendDataTo(index, buffer.ToArray());
        }
    }
}
