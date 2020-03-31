// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

namespace JellyFish.PlayerInput
{
    [Serializable]
    public class GameInput
    {
        /// <summary>
        ///     The event associated with this game input.
        /// </summary>
        public UltEvent Event;

        /// <summary>
        ///     The keys for this game input.
        /// </summary>
        public List<InputKey> Keys = new List<InputKey>();

        /// <summary>
        ///     Checks whether all keys for this game input match the associated input type.
        /// </summary>
        /// <returns></returns>
        public bool KeysActive()
        {
            foreach(InputKey inputKey in Keys)
                switch(inputKey.InputType)
                {
                    case InputKey.InputTypes.KeyDown:

                        if(!Input.GetKeyDown(inputKey.Key)) return false;

                        break;
                    case InputKey.InputTypes.KeyUp:

                        if(!Input.GetKeyUp(inputKey.Key)) return false;

                        break;
                    case InputKey.InputTypes.KeyPressed:

                        if(!Input.GetKey(inputKey.Key)) return false;

                        break;
                }

            return true;
        }

        /// <summary>
        ///     Invokes the associated event.
        /// </summary>
        public void InvokeEvent()
        {
            Event.Invoke();
        }

        /// <summary>
        ///     Checks the game input state. Invokes the associated event when the game input is active.
        /// </summary>
        public void CheckInputState()
        {
            if(KeysActive()) InvokeEvent();
        }
    }
}