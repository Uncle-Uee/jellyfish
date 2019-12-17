/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using System;
using UnityEngine.SceneManagement;

// ReSharper disable once CheckNamespace
namespace JellyFish.Data.Primitive
{
    [Serializable]
    public class SceneReference
    {
        public Scene         ConstantValue;
        public bool          UseConstant = true;
        public SceneVariable Variable;

        public SceneReference()
        {
        }

        public SceneReference(Scene value)
        {
            UseConstant   = true;
            ConstantValue = value;
        }

        public Scene Value
        {
            get => UseConstant ? ConstantValue : Variable.Value;
            set
            {
                if (UseConstant)
                    ConstantValue   = value;
                else Variable.Value = value;
            }
        }

        public static implicit operator Scene(SceneReference reference)
        {
            return reference.Value;
        }
    }
}