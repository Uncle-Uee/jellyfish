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
    public class MaterialReference
    {
        public Material         ConstantValue;
        public bool             UseConstant = true;
        public MaterialVariable Variable;

        public MaterialReference()
        {
        }

        public MaterialReference(Material value)
        {
            UseConstant   = true;
            ConstantValue = value;
        }

        public Material Value
        {
            get => UseConstant ? ConstantValue : Variable.Value;
            set
            {
                if (UseConstant)
                {
                    ConstantValue = value;
                }
                else
                {
                    Variable.Value = value;
                }
            }
        }

        public static implicit operator Material(MaterialReference reference)
        {
            return reference.Value;
        }
    }
}