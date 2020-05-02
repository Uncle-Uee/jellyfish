/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using System.Collections;
using System.Globalization;
using SOFlow.Data.Primitives;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.Monitor.FPS
{
    [ExecuteAlways]
    public class FPSMonitor : MonoBehaviour
    {
        #region VARIABLES

        /// <summary>
        /// Show FPS Log.
        /// </summary>
        [Header("Show FPS")]
        public BoolField ShowFPS;

        /// <summary>
        /// FPS Label.
        /// </summary>
        private string _fpsLabel = "";

        /// <summary>
        /// FPS Value.
        /// </summary>
        private float _count;

        #endregion

        #region UNITY METHODS

        private IEnumerator Start()
        {
            GUI.depth = 2;

            while (ShowFPS)
            {
                if (Time.timeScale == 1)
                {
                    yield return new WaitForSeconds(0.1f);

                    _count    = (1 / Time.deltaTime);
                    _fpsLabel = Mathf.Round(_count).ToString(CultureInfo.InstalledUICulture);
                }
                else
                {
                    _fpsLabel = "Pause";
                }

                yield return new WaitForSeconds(0.5f);
            }
        }

        #endregion

        #region UNTIY ON GUI METHOD

        private void OnGUI()
        {
            if (ShowFPS)
            {
                GUI.Label(new Rect(Screen.width - 32, 4, 100, 25), _fpsLabel);
            }
        }

        #endregion
    }
}