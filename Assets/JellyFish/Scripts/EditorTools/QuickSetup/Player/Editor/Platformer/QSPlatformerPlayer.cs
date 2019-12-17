/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

#if UNITY_EDITOR
using JellyFish.EditorTools.QuickSetup.Utilities;
using UnityEditor;

namespace JellyFish.EditorTools.QuickSetup.Player
{
    public static class QSPlatformerPlayer
    {
        #region METHODS

        /// <summary>
        /// Create Platformer Player.
        /// </summary>
        [MenuItem("Assets/QuickSetup/Demo", priority = 10)]
        public static void CreatePlatformerPlayer()
        {
            QuickSetupUtilities.CreateGameObject();
        }

        #endregion
    }
}
#endif