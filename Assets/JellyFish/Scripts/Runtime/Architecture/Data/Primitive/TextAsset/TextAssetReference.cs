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
    public class TextAssetReference
    {
        public TextAsset         ConstantValue;
        public bool              UseConstant = true;
        public TextAssetVariable Variable;

        public TextAssetReference()
        {
        }

        public TextAssetReference(TextAsset value)
        {
            UseConstant   = true;
            ConstantValue = value;
        }

        public TextAsset Value
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

        public static implicit operator TextAsset(TextAssetReference reference)
        {
            return reference.Value;
        }
    }
}