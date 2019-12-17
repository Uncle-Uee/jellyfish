/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.Data.Primitive
{
    [CreateAssetMenu(menuName = "JellyFish/Data/Primitive/TextAsset", fileName = "TextAsset")]
    public class TextAssetVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public TextAsset Value;

        public void SetValue(TextAsset value)
        {
            Value = value;
        }

        public void SetValue(TextAssetVariable value)
        {
            Value = value.Value;
        }
    }
}