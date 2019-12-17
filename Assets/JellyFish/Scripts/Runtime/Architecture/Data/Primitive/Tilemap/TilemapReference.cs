/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using System;
using UnityEngine.Tilemaps;

// ReSharper disable once CheckNamespace
namespace JellyFish.Data.Primitive
{
    [Serializable]
    public class TilemapReference
    {
        public Tilemap         ConstantValue;
        public bool            UseConstant = true;
        public TilemapVariable Variable;

        public TilemapReference()
        {
        }

        public TilemapReference(Tilemap value)
        {
            UseConstant   = true;
            ConstantValue = value;
        }

        public Tilemap Value
        {
            get => UseConstant ? ConstantValue : Variable.Value;
            set
            {
                if (UseConstant)
                    ConstantValue   = value;
                else Variable.Value = value;
            }
        }

        public static implicit operator Tilemap(TilemapReference reference)
        {
            return reference.Value;
        }
    }
}