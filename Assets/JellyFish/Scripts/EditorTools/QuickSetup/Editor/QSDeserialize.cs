/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

#if UNITY_EDITOR
using JellyFish.EditorTools.QuickSetup.Utilities;
using UnityEditor;

namespace JellyFish.EditorTools.QuickSetup
{
    public static class QSDeserialize
    {
        #region METHODS

        /// <summary>
        /// Deserialize Json Settings File.
        /// </summary>
        [MenuItem("Assets/QuickSetup/Deserialize", priority = 20)]
        public static void CreatePlatformerPlayer()
        {
            QSUtilities.CreateGameObject();
        }

        #endregion
    }
}
#endif