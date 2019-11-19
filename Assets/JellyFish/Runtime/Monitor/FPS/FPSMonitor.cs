/*
 *
 * Created By: Ubaidullah Effendi-Emjedi, Uee
 * Alias: Uee
 * Modified By:
 *
 * Last Modified: 09 November 2019
 *
 */

using System.Collections;
using System.Globalization;
using JellyFish.Data.Primitive;
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
        public BooleanReference ShowFPS;

        /// <summary>
        /// FPS Label.
        /// </summary>
        private string label = "";

        /// <summary>
        /// FPS Value.
        /// </summary>
        private float count;

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
                    count = (1 / Time.deltaTime);
                    label = Mathf.Round(count).ToString(CultureInfo.InstalledUICulture);
                }
                else
                {
                    label = "Pause";
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
                GUI.Label(new Rect(Screen.width - 32, 4, 100, 25), label);
            }
        }

        #endregion
    }
}