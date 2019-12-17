/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using System;
using JellyFish.Data.Primitive;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.Data.Primitive
{
    [Serializable]
    public class ColourReference
    {
        public Color         ConstantValue;
        public bool          UseConstant = true;
        public ColourVariable Variable;

        public ColourReference()
        {
        }

        public ColourReference(Color value)
        {
            UseConstant   = true;
            ConstantValue = value;
        }

        public Color Value
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

        public static implicit operator Color(ColourReference reference)
        {
            return reference.Value;
        }
    }
}