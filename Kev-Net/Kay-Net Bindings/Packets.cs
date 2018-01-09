
namespace Kay_Net_Bindings
{
    /* ----------------------------------------
    * |Constants.cs Class by Kevin Kaymak © 2017|
    * ----------------------------------------
    * This class is a shared by the server and client
    * project. All values changed here are changed 
    * automatically on both server and client. This class
    * got the Packetlist which server and client listens to.
    * This class assigns the identifier to the packet itself.
    */

    //ServerPacket list which the client has to listen to!
    //ServerPackets get send from server to client!
    public enum ServerPackets
    {
        SChatMsg = 1, // sample packet to send a text from server to client.
    }
    //ClientPacket list which the server has to listen to!
    //ClientPackets get send from client to server!
    public enum ClientPackets
    {
        CChatMsg = 1, // sample packet to send a text from client to server.
    }
}
