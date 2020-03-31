// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System;
using UnityEngine;

namespace SOFlow.Data.Primitives
{
    [Serializable]
    public class DataField
    {
        /// <summary>
        ///     Determines whether this field references volatile or non-volatile data.
        /// </summary>
        [HideInInspector]
        public bool UseConstant = true;

        /// <summary>
        ///     Gets the variable data.
        /// </summary>
        /// <returns></returns>
        public virtual PrimitiveData GetVariable()
        {
            return null;
        }
    }
}