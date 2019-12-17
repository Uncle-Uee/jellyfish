/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.Data.Primitive
{
    [CreateAssetMenu(fileName = "Colour", menuName = "JellyFish/Data/Primitive/Color")]
    public class ColourVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public Color Value;

        public void SetValue(Color value)
        {
            Value = value;
        }

        public void SetValue(ColourVariable value)
        {
            Value = value.Value;
        }
    }
}