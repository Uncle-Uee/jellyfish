// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using UnityEngine;

namespace SOFlow.Utilities
{
    public static class ObjectLifeHandler
    {
	    /// <summary>
	    ///     Instantiates the given target with the specified parameters.
	    /// </summary>
	    /// <param name="target"></param>
	    /// <param name="parent"></param>
	    /// <param name="position"></param>
	    /// <param name="rotation"></param>
	    /// <param name="localSpace"></param>
	    /// <param name="active"></param>
	    public static void Instantiate(GameObject target,     Transform parent, Vector3 position, Vector3 rotation,
                                       bool       localSpace, bool      active)
        {
            GameObject instantiatedObject = Object.Instantiate(target, parent);

            if(localSpace)
            {
                instantiatedObject.transform.localPosition    = position;
                instantiatedObject.transform.localEulerAngles = rotation;
            }
            else
            {
                instantiatedObject.transform.position    = position;
                instantiatedObject.transform.eulerAngles = rotation;
            }

            instantiatedObject.SetActive(active);
        }

	    /// <summary>
	    ///     Destroys the given target gameobject.
	    /// </summary>
	    /// <param name="target"></param>
	    public static void Destroy(GameObject target)
        {
            Object.Destroy(target);
        }

	    /// <summary>
	    ///     Destroys the given target component.
	    /// </summary>
	    /// <param name="target"></param>
	    public static void Destroy(MonoBehaviour target)
        {
            Object.Destroy(target);
        }
    }
}