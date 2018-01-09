//using the shared project to access shared classes.
using Kay_Net_Bindings;
using Kev_Net_SocketBuffer;

namespace Kay_Net_Client
{
    /* ----------------------------------------
     * |SocketSend.cs Class by Kevin Kaymak © 2017|
     * ----------------------------------------
     * This class is needed to send packets from the client
     * to the server over the network!
     */
    class SocketSend
    {
        //Sample Send Method. send data to the connected server.
        //If you do want send a packet over the network from the client
        //you always have to design a sending method like the sample one.
        //Optinal: (string chatMsg to assign a written text to send over network)
        public static void SendThankYou()
        {
            //create a new buffer to write data in it!
            SocketBuffer buffer = new SocketBuffer();
            //Assign the identifier to the packet! Never forget this
            //else the server does not now what to do with the received packet.
            buffer.WriteInteger((int)ClientPackets.CChatMsg);
            //add your chatMsg to the packet, so the client can read it out.
            buffer.WriteString("Thank you im glad that I'm able to connect to the server!");
            //send data over the network to the connected server. convert the buffer to a byte[] to make it available to send over the network.
            Network.SendData(buffer.ToArray());
        }
    }
}