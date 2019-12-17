/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.Data.Primitive
{
    [CreateAssetMenu(fileName = "Object", menuName = "JellyFish/Data/Primitive/Object")]
    public class ObjectVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public Object Value;

        public void SetValue(Object value)
        {
            Value = value;
        }

        public void SetValue(ObjectVariable objectValue)
        {
            Value = objectValue.Value;
        }
    }
}