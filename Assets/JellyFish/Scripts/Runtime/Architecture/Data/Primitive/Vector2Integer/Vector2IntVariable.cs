/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.Data.Primitive
{
    [CreateAssetMenu(menuName = "JellyFish/Data/Primitive/Vector2Int", fileName = "Vector2Int")]
    public class Vector2IntVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif

        public Vector2Int Value;

        public void SetValue(Vector2Int value)
        {
            Value = value;
        }

        public void SetValue(Vector2IntVariable value)
        {
            Value = value.Value;
        }

        public void ApplyChange(Vector2Int amount)
        {
            Value += amount;
        }

        public void ApplyChange(Vector2IntVariable amount)
        {
            Value += amount.Value;
        }
    }
}