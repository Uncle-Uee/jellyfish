/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */
using JellyFish.Data.Primitive;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.Management.GameManagement
{
    public class PersistentGameManager : MonoBehaviour
    {
        #region VARIABLES

        /// <summary>
        /// Don't Destroy this Object On Load.
        /// </summary>
        [Header("Don't Destroy On Load")]
        public BooleanReference DontDestroy;

        /// <summary>
        /// Indicates if this is the Primary Game Manager.
        /// </summary>
        private bool _primaryGameManager;

        /// <summary>
        /// Indicate if the Main Game Manager is Registered.
        /// </summary>
        private bool _hasRegisteredGameManager;

        #endregion

        #region UNITY METHODS

        public void Awake()
        {
            if (DontDestroy && !_hasRegisteredGameManager)
            {
                DontDestroyOnLoad(gameObject);
                _hasRegisteredGameManager = true;
                _primaryGameManager       = true;
            }
            else if (DontDestroy && !_primaryGameManager)
            {
                Destroy(gameObject);
            }
        }

        #endregion
    }
}