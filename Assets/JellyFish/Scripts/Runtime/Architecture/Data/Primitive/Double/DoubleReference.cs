/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using System;

// ReSharper disable once CheckNamespace
namespace JellyFish.Data.Primitive
{
    [Serializable]
    public class DoubleReference
    {
        public double         ConstantValue;
        public bool           UseConstant = true;
        public DoubleVariable Variable;

        public DoubleReference()
        {
        }

        public DoubleReference(double value)
        {
            UseConstant   = true;
            ConstantValue = value;
        }

        public double Value
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

        public static implicit operator double(DoubleReference reference)
        {
            return reference.Value;
        }
    }
}