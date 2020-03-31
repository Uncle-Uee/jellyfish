// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using UnityEngine;

namespace JellyFish.Audio
{
    public class AudioBridge : MonoBehaviour
    {
	    /// <summary>
	    ///     The audio link.
	    /// </summary>
	    public AudioLink AudioLink;

	    /// <summary>
	    ///     The audio source.
	    /// </summary>
	    public AudioSource AudioSource;

        private void Awake()
        {
            AudioLink.AudioSource = AudioSource;
        }

        private void Update()
        {
            AudioLink.ClearClipCache();
        }

#if UNITY_EDITOR
        /// <summary>
        ///     Adds a Audio Bridge to the scene.
        /// </summary>
        [UnityEditor.MenuItem("GameObject/SOFlow/Audio/Add Audio Bridge", false, 10)]
        public static void AddComponentToScene()
        {
	        GameObject _gameObject = new GameObject("Audio Bridge", typeof(AudioBridge));

	        if(UnityEditor.Selection.activeTransform != null)
	        {
		        _gameObject.transform.SetParent(UnityEditor.Selection.activeTransform);
	        }

	        UnityEditor.Selection.activeGameObject = _gameObject;
        }
#endif
    }
}