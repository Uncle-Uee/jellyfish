/*
 *
 * Created By: Ubaidullah Effendi-Emjedi, Uee
 * Alias: Uee
 * Modified By:
 *
 * Last Modified: 09 November 2019
 *
 */

using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.ObjectPool.Interfaces
{
    public interface IPool
    {
        #region PROPERTIES

        /// <summary>
        /// GameObject Pool Reference.
        /// </summary>
        List<GameObject> Pool { get; set; }

        /// <summary>
        /// Active Pool Reference.
        /// </summary>
        List<GameObject> ActivePool { get; set; }

        int Index { get; set; }

        #endregion
    }
}