/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */


using System;

// ReSharper disable once CheckNamespace
namespace JellyFish.Data.Primitive
{
    [Serializable]
    public class BooleanReference
    {
        public bool            ConstantValue;
        public bool            UseConstant = true;
        public BooleanVariable Variable;

        public BooleanReference()
        {
        }

        public BooleanReference(bool value)
        {
            UseConstant = true;
            ConstantValue = value;
        }

        public bool Value
        {
            get => UseConstant ? ConstantValue : Variable.Value;
            set
            {
                if (UseConstant)
                    ConstantValue = value;
                else Variable.Value = value;
            }
        }

        public static implicit operator bool(BooleanReference reference)
        {
            return reference.Value;
        }
    }
}