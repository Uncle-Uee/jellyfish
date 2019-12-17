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
    public class GameObjectReference
    {
        /// <summary>
        /// GameObject Constant.
        /// </summary>
        public GameObject ConstantValue;

        /// <summary>
        /// GameObject ScriptableObject Variable
        /// </summary>
        public GameObjectVariable Variable;

        /// <summary>
        /// Use Constant Flag.
        /// </summary>
        public bool UseConstant = true;

        public GameObjectReference()
        {
        }

        public GameObjectReference(GameObject value)
        {
            UseConstant = true;
            ConstantValue = value;
        }

        /// <summary>
        /// GameObject Value Reference.
        /// </summary>
        public GameObject Value
        {
            get => UseConstant ? ConstantValue : Variable.GameObject;
            set
            {
                if (UseConstant)
                {
                    ConstantValue = value;
                }
                else
                {
                    Variable.GameObject = value;
                }
            }
        }

        /// <summary>
        /// Implicit GameObject Operator.
        /// </summary>
        /// <param name="reference"></param>
        /// <returns></returns>
        public static implicit operator GameObject(GameObjectReference reference)
        {
            return reference.Value;
        }
    }
}