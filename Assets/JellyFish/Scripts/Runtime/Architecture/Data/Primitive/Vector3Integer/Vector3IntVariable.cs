/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.Data.Primitive
{
    [CreateAssetMenu(menuName = "JellyFish/Data/Primitive/Vector3Int", fileName = "Vector3Int")]
    public class Vector3IntVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif

        public Vector3Int Value;

        public void SetValue(Vector3Int value)
        {
            Value = value;
        }

        public void SetValue(Vector3IntVariable value)
        {
            Value = value.Value;
        }

        public void ApplyChange(Vector3Int amount)
        {
            Value += amount;
        }

        public void ApplyChange(Vector3IntVariable amount)
        {
            Value += amount.Value;
        }
    }
}