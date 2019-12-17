/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using UnityEngine;
using UnityEngine.Tilemaps;

// ReSharper disable once CheckNamespace
namespace JellyFish.Data.Primitive
{
    [CreateAssetMenu(menuName = "JellyFish/Data/Primitive/Tilemap", fileName = "Tilemap")]
    public class TilemapVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public Tilemap Value;

        public void SetValue(Tilemap value)
        {
            Value = value;
        }

        public void SetValue(TilemapVariable value)
        {
            Value = value.Value;
        }
    }
}