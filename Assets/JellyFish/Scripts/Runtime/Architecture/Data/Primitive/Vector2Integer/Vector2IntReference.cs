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
    public class Vector2IntReference
    {
        public Vector2Int         ConstantValue;
        public bool               UseConstant = true;
        public Vector2IntVariable Variable;

        public Vector2IntReference()
        {
        }

        public Vector2IntReference(Vector2Int value)
        {
            UseConstant   = true;
            ConstantValue = value;
        }

        public Vector2Int Value
        {
            get => UseConstant ? ConstantValue : Variable.Value;
            set
            {
                if (UseConstant)
                    ConstantValue   = value;
                else Variable.Value = value;
            }
        }

        public static implicit operator Vector2Int(Vector2IntReference reference)
        {
            return reference.Value;
        }
    }
}