/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using UltEvents;
using UnityEngine;

namespace JellyFish.Monitor.ScreenSize
{
    [CreateAssetMenu(fileName = "Resolution State", menuName = "JellyFish/Monitor/ScreenSize/Resolution State")]
    public class ResolutionState : ScriptableObject
    {
        #region VARIABLES

        /// <summary>
        /// Designed Screen Resolution.
        /// </summary>
        [Header("Resolution Parameters")]
        public Vector2 DesignScreenResolution;

        /// <summary>
        /// On Screen Resolution Changed.
        /// </summary>
        public UltEvent OnScreenResolutionChanged;

        
        /// <summary>
        /// Current Screen Resolution.
        /// </summary>
        private Vector2 _currentScreenResolution;

        #endregion


        #region PROPERTIES

        /// <summary>
        /// Current Screen Resolution.
        /// </summary>
        public Vector2 CurrentScreenResolution
        {
            get => _currentScreenResolution;
            set
            {
                _currentScreenResolution = value;
                OnScreenResolutionChanged?.Invoke();
            }
        }

        #endregion
    } 
}