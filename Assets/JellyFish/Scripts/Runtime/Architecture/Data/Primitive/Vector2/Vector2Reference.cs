/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.Data.Primitive
{
    [Serializable]
    public class Vector2Reference
    {
        public Vector2         ConstantValue;
        public bool            UseConstant = true;
        public Vector2Variable Variable;

        public Vector2Reference()
        {
        }

        public Vector2Reference(Vector2 value)
        {
            UseConstant = true;
            ConstantValue = value;
        }

        public Vector2 Value
        {
            get => UseConstant ? ConstantValue : Variable.Value;
            set
            {
                if (UseConstant)
                    ConstantValue = value;
                else Variable.Value = value;
            }
        }

        public static implicit operator Vector2(Vector2Reference reference)
        {
            return reference.Value;
        }
    }
}