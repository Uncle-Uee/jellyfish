// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System.Collections;
using UnityEngine;

namespace JellyFish.Audio
{
    public class AudioController : MonoBehaviour
    {
	    /// <summary>
	    ///     Plays the given audio clip in reverse.
	    /// </summary>
	    /// <param name="audioSource"></param>
	    /// <param name="clip"></param>
	    /// <param name="sampleOffset"></param>
	    public void PlayAudioReversed(AudioSource audioSource, AudioClip clip, float sampleOffset)
        {
            audioSource.clip        = clip;
            audioSource.timeSamples = (int)(clip.samples * sampleOffset);
            audioSource.pitch       = -1;
            audioSource.Play();
        }

	    /// <summary>
	    ///     Plays the given audio clip with a random pitch.
	    /// </summary>
	    /// <param name="audioSource"></param>
	    /// <param name="clip"></param>
	    /// <param name="volume"></param>
	    /// <param name="pitchRange"></param>
	    public void PlayAudioAtRandomPitch(AudioSource audioSource, AudioClip clip, float volume, Vector2 pitchRange)
        {
            audioSource.clip   = clip;
            audioSource.volume = volume;
            audioSource.pitch  = Random.Range(pitchRange.x, pitchRange.y);
            audioSource.Play();
        }

	    /// <summary>
	    ///     Fades the given audio source.
	    /// </summary>
	    /// <param name="audioSource"></param>
	    /// <param name="targetVolume"></param>
	    /// <param name="fadeTime"></param>
	    public void FadeAudio(AudioSource audioSource, float targetVolume, float fadeTime)
        {
            StartCoroutine(FadeAudioOverTime(audioSource, targetVolume, fadeTime));
        }

        private IEnumerator FadeAudioOverTime(AudioSource audioSource, float targetVolume, float fadeTime)
        {
            float startingVolume = audioSource.volume;

            float startTime = Time.realtimeSinceStartup;
            float endTime   = startTime + fadeTime;

            while(Time.realtimeSinceStartup < endTime)
            {
                float percentage = (Time.realtimeSinceStartup - startTime) /
                                   (endTime                   - startTime);

                audioSource.volume = Mathf.Lerp(startingVolume, targetVolume, percentage);

                yield return null;
            }

            audioSource.volume = targetVolume;
        }

#if UNITY_EDITOR
        /// <summary>
        ///     Adds a Audio Controller to the scene.
        /// </summary>
        [UnityEditor.MenuItem("GameObject/SOFlow/Audio/Add Audio Controller", false, 10)]
        public static void AddComponentToScene()
        {
	        GameObject _gameObject = new GameObject("Audio Controller", typeof(AudioController));

	        if(UnityEditor.Selection.activeTransform != null)
	        {
		        _gameObject.transform.SetParent(UnityEditor.Selection.activeTransform);
	        }

	        UnityEditor.Selection.activeGameObject = _gameObject;
        }
#endif
    }
}