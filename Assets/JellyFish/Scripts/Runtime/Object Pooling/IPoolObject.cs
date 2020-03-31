// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using UnityEngine;

namespace JellyFish.ObjectPooling
{
    public interface IPoolObject<T> : IPoolObjectRoot
    {
	    /// <summary>
	    /// Instantiates an instance of this object.
	    /// </summary>
	    /// <param name="container"></param>
	    /// <returns></returns>
	    T Instantiate(Transform container);
    }

    public interface IPoolObjectRoot
    {
	    /// <summary>
	    ///     The object ID.
	    /// </summary>
	    string ID
	    {
		    get;
	    }
	    
	    /// <summary>
	    ///     Activates this object.
	    /// </summary>
	    void ActivateObject();

	    /// <summary>
	    ///     Deactivates this object.
	    /// </summary>
	    void DeactivateObject();

	    /// <summary>
	    /// Gets the Unity object instance of this object.
	    /// </summary>
	    /// <returns></returns>
	    Object GetObjectInstance();
    }
}