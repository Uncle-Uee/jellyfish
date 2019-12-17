/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using System;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace JellyFish.Data.Primitive
{
    [Serializable]
    public class ObjectReference
    {
        public Object         ConstantValue;
        public bool           UseConstant = true;
        public ObjectVariable Variable;

        public ObjectReference()
        {
        }

        public ObjectReference(Object value)
        {
            UseConstant   = true;
            ConstantValue = value;
        }

        public Object Value
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

        public static implicit operator Object(ObjectReference reference)
        {
            return reference.Value;
        }
    }
}