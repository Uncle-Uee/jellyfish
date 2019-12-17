/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using System;

namespace JellyFish.Data.Primitive
{
    [Serializable]
    public class StringReference
    {
        public string         ConstantValue;
        public bool           UseConstant = true;
        public StringVariable Variable;

        public StringReference()
        {
        }

        public StringReference(string value)
        {
            UseConstant   = true;
            ConstantValue = value;
        }

        public string Value
        {
            get => UseConstant ? ConstantValue : Variable.Value;
            set
            {
                if (UseConstant)
                    ConstantValue   = value;
                else Variable.Value = value;
            }
        }

        public static implicit operator string(StringReference reference)
        {
            return reference.Value;
        }
    }
}