/*
 *
 * Created By: Ubaidullah Effendi-Emjedi
 * Alias: Uee
 * Modified By: 
 *
 * Last Modified: 20 June 2019
 *
 *
 * This software is released under the terms of the
 * GNU license. See https://www.gnu.org/licenses/#GPL
 * for more information.
 *
 */

using UnityEngine;

public static class GameObjectExtension
{
    #region GAMEOBJECT EXTENSIONS METHODS

    /// <summary>
    /// Get Parent of GameObject.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static Transform Parent(this GameObject gameObject)
    {
        return gameObject.transform.parent;
    }

    /// <summary>
    /// Parent GameObject.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static GameObject ParentGameObject(this GameObject gameObject)
    {
        return gameObject.transform.parent.gameObject;
    }

    #endregion
}