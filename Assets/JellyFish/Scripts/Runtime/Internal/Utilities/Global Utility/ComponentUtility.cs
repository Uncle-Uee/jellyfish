/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.Internal.Utilities
{
    /// <summary>
    /// Constant Variables, Methods etc
    /// </summary>
    public static class ComponentUtility
    {
        #region GLOBAL COMPONENTS METHODS

        /// <summary>
        /// Find a T Component on the GameObject, its Parent or Children.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FindComponent<T>(GameObject gameObject) where T : Component
        {
            return gameObject.ParentGameObject().GetComponent<T>() ??
                   gameObject.ParentGameObject().GetComponentInChildren<T>() ??
                   gameObject.GetComponent<T>() ??
                   gameObject.GetComponentInParent<T>() ??
                   gameObject.GetComponentInChildren<T>();
        }


        /// <summary>
        /// Find a T Component on the GameObject, its Parent or Children.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="component"></param>
        /// <typeparam name="T"></typeparam>
        public static void FindComponent<T>(GameObject gameObject, out T component) where T : Component
        {
            component = gameObject.ParentGameObject().GetComponent<T>() ??
                        gameObject.ParentGameObject().GetComponentInChildren<T>() ??
                        gameObject.GetComponent<T>() ?? gameObject.GetComponentInParent<T>() ??
                        gameObject.GetComponentInChildren<T>();

//
//            if (!component)
//            {
//                component = gameObject.GetComponentInParent<T>();
//            }
//
//            if (!component)
//            {
//                component = gameObject.GetComponentInChildren<T>() ??
//                            gameObject.ParentGameObject().GetComponent<T>() ??
//                            gameObject.ParentGameObject().GetComponentInChildren<T>();
//            }
        }


        /// <summary>
        /// Find T Components on the GameObject, its Parent or Children.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] FindComponents<T>(GameObject gameObject) where T : Component
        {
            return gameObject.ParentGameObject().GetComponents<T>() ??
                   gameObject.ParentGameObject().GetComponentsInChildren<T>() ??
                   gameObject.GetComponents<T>() ??
                   gameObject.GetComponentsInParent<T>() ??
                   gameObject.GetComponentsInChildren<T>();
        }


        /// <summary>
        /// Find T Components on the GameObject, its Parent or Children.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="components"></param>
        /// <typeparam name="T"></typeparam>
        public static void FindComponents<T>(GameObject gameObject, out T[] components) where T : Component
        {
            components = gameObject.ParentGameObject().GetComponents<T>() ??
                         gameObject.ParentGameObject().GetComponentsInChildren<T>();

            if (components == null)
            {
                components = gameObject.GetComponentsInParent<T>();
            }

            if (components == null)
            {
                components = gameObject.GetComponentsInChildren<T>() ??
                             gameObject.ParentGameObject().GetComponents<T>() ??
                             gameObject.ParentGameObject().GetComponentsInChildren<T>();
            }
        }

        #endregion
    }
}