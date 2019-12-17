/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using System;

// ReSharper disable once CheckNamespace
namespace JellyFish.Data.Primitive
{
    [Serializable]
    public class IntReference
    {
        public int         ConstantValue;
        public bool        UseConstant = true;
        public IntVariable Variable;

        public IntReference()
        {
        }

        public IntReference(int value)
        {
            UseConstant   = true;
            ConstantValue = value;
        }

        public int Value
        {
            get => UseConstant ? ConstantValue : Variable.Value;
            set
            {
                if (UseConstant)
                    ConstantValue   = value;
                else Variable.Value = value;
            }
        }

        public static implicit operator int(IntReference reference)
        {
            return reference.Value;
        }
    }
}