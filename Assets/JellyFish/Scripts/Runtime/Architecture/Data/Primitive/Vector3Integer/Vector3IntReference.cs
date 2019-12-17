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
    public class Vector3IntReference
    {
        public Vector3Int         ConstantValue;
        public bool               UseConstant = true;
        public Vector3IntVariable Variable;

        public Vector3IntReference()
        {
        }

        public Vector3IntReference(Vector3Int value)
        {
            UseConstant   = true;
            ConstantValue = value;
        }

        public Vector3Int Value
        {
            get => UseConstant ? ConstantValue : Variable.Value;
            set
            {
                if (UseConstant)
                    ConstantValue   = value;
                else Variable.Value = value;
            }
        }

        public static implicit operator Vector3Int(Vector3IntReference reference)
        {
            return reference.Value;
        }
    }
}