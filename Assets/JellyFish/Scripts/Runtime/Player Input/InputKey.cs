// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System;
using UnityEngine;

namespace JellyFish.PlayerInput
{
    [Serializable]
    public class InputKey
    {
        public enum InputTypes
        {
            KeyDown    = 0,
            KeyUp      = 1,
            KeyPressed = 2
        }

        /// <summary>
        ///     The type of input.
        /// </summary>
        public InputTypes InputType = InputTypes.KeyDown;

        /// <summary>
        ///     The input key.
        /// </summary>
        public KeyCode Key;
    }
}