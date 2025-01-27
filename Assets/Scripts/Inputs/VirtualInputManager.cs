﻿using Game.singleton;


namespace Game.Inputs
{
    /// <summary>
    /// This script will be used to control the player and
    /// can be controlled by any input script, so that
    /// it is cross-platform friendly.
    /// Note that this script will get instantiated in the scene at the start of the game if
    /// a monobehaviour script tries to access it
    /// </summary>
    public class VirtualInputManager : Singleton<VirtualInputManager>
    {
        public bool jump { get; set; }
        public bool moveLeft { get; set; }
        public bool moveRight { get; set; }
        public bool moveUp { get; set; }
        public bool moveDown { get; set; }

        /// <summary>
        /// Is true when player holds down
        /// shift or when player is "run" mode
        /// </summary>
        /// <value></value>
        public bool turbo { get; set; }
        public bool secondJump { get; set; }




    }
}
