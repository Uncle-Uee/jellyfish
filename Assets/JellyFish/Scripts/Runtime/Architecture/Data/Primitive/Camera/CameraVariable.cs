/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.Data.Primitive
{
    [CreateAssetMenu(menuName = "JellyFish/Data/Primitive/Camera", fileName = "Camera")]
    public class CameraVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public Camera Value;

        /// <summary>
        /// Set Camera Value
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(Camera value)
        {
            Value = value;
        }

        /// <summary>
        /// Set Camera Reference Value
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(CameraVariable value)
        {
            Value = value.Value;
        }
    }
}