using System;
using System.Collections.Generic;
using System.Text;

namespace Kay_Net_Bindings
{
    class Constants
    {
        /* ----------------------------------------
        * |Constants.cs Class by Kevin Kaymak © 2017|
        * ----------------------------------------
        * This class is a shared by the server and client
        * project. All values changed here are changed 
        * automatically on both server and client. This class
        * has all game settings which have to be shared. Make
        * sure you set the Reference on Server and Client for
        * the "Kay-Net Bindings" project also use "using Kay-
        * Net Bindings" to access all variables of this class
        * by calling "Constants." or "Kay_Net_Bindings.Constants."
        */

        //set the max. of connected players you want.
        public const int MAX_PLAYERS = 100;
    }
}
