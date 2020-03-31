// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System.Collections.Generic;
using UnityEngine;

namespace SOFlow.Utilities
{
	/// <inheritdoc />
	/// <summary>
	///     Component used to maintain a list of objects required to persist across scene loads.
	/// </summary>
	public class PersistingAssets : MonoBehaviour
    {
	    /// <summary>
	    ///     The instance of this component.
	    /// </summary>
	    private static PersistingAssets _instance;

	    /// <summary>
	    ///     The list of persistent assets.
	    /// </summary>
	    public List<ScriptableObject> PersistentAssets = new List<ScriptableObject>();

        private void Awake()
        {
            if(!_instance)
            {
                DontDestroyOnLoad(gameObject);
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}