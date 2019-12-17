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
    public class Vector3Reference
    {
        public Vector3         ConstantValue;
        public bool            UseConstant = true;
        public Vector3Variable Variable;

        public Vector3Reference()
        {
        }

        public Vector3Reference(Vector3 value)
        {
            UseConstant   = true;
            ConstantValue = value;
        }

        public Vector3 Value
        {
            get => UseConstant ? ConstantValue : Variable.Value;
            set
            {
                if (UseConstant)
                    ConstantValue   = value;
                else Variable.Value = value;
            }
        }

        public static implicit operator Vector3(Vector3Reference reference)
        {
            return reference.Value;
        }
    }
}