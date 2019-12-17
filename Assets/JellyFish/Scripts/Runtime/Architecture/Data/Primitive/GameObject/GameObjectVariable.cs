/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.Data.Primitive
{
    [CreateAssetMenu(menuName = "JellyFish/Data/Primitive/GameObject", fileName = "GameObject")]
    public class GameObjectVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        /// <summary>
        /// GameObject Variable
        /// </summary>
        public GameObject GameObject;

        /// <summary>
        /// Set this GameObject.
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(GameObject value)
        {
            GameObject = value;
        }

        /// <summary>
        /// Set this GameObject Variable.
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(GameObjectVariable value)
        {
            GameObject = value.GameObject;
        }
    }
}