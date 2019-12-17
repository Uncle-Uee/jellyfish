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
    public class GridReference
    {
        public Grid         ConstantValue;
        public bool         UseConstant = true;
        public GridVariable Variable;

        public GridReference()
        {
        }

        public GridReference(Grid value)
        {
            UseConstant   = true;
            ConstantValue = value;
        }

        public Grid Value
        {
            get => UseConstant ? ConstantValue : Variable.Value;
            set
            {
                if (UseConstant)
                    ConstantValue   = value;
                else Variable.Value = value;
            }
        }

        public static implicit operator Grid(GridReference reference)
        {
            return reference.Value;
        }
    }
}