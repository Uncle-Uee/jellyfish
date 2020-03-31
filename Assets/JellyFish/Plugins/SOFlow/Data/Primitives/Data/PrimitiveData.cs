// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using SOFlow.ScriptableObjects;
using UnityEngine;

namespace SOFlow.Data.Primitives
{
    public class PrimitiveData : DropdownScriptableObject
    {
        /// <summary>
        /// The developer description for this primitive data.
        /// </summary>
        [Multiline]
        public string Description;

#if UNITY_EDITOR
        public override void OnValidate()
        {
            base.OnValidate();
            
            // Resync the Play Mode safe representation with the
            // true asset value during editing.
            ResetValue();
        }
#endif
        
        /// <summary>
        ///     Returns the value of this data.
        /// </summary>
        /// <returns></returns>
        public virtual object GetValueData()
        {
            return null;
        }

        /// <summary>
        /// Resets the value of this data to its default state.
        /// </summary>
        public virtual void ResetValue()
        {
        }
    }
}