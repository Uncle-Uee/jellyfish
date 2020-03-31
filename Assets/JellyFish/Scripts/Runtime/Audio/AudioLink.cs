// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System.Collections.Generic;
using System.Linq;
using SOFlow.Data.Primitives;
using UnityEngine;

namespace JellyFish.Audio
{
    [CreateAssetMenu(menuName = "SOFlow/Audio/Audio Link")]
    public class AudioLink : ScriptableObject
    {
	    /// <summary>
	    ///     The audio clip cache.
	    /// </summary>
	    private readonly Dictionary<AudioClip, int> _clipCached = new Dictionary<AudioClip, int>();

	    /// <summary>
	    ///     Indicates whether a clip is present within the clip cache.
	    /// </summary>
	    private bool _clipPresent;

	    /// <summary>
	    ///     The audio source.
	    /// </summary>
	    public AudioSource AudioSource;

	    /// <summary>
	    ///     The maximum amount of allowed concurrent audio clips.
	    /// </summary>
	    public IntField MaximumConcurrentClips;

	    /// <summary>
	    ///     Plays the given audio clip.
	    /// </summary>
	    /// <param name="clip"></param>
	    public void PlayAudio(AudioClip clip)
        {
            int clipCount;

            if(_clipCached.TryGetValue(clip, out clipCount))
            {
                if(clipCount < MaximumConcurrentClips)
                {
                    _clipCached[clip] = ++clipCount;
                    AudioSource.PlayOneShot(clip);
                    _clipPresent = true;
                }
            }
            else
            {
                _clipCached.Add(clip, 1);
                AudioSource.PlayOneShot(clip);
                _clipPresent = true;
            }
        }

	    /// <summary>
	    ///     Clears the clip cache.
	    /// </summary>
	    public void ClearClipCache()
        {
            if(_clipPresent)
            {
                foreach(AudioClip clip in _clipCached.Keys.ToList()) _clipCached[clip] = 0;

                _clipPresent = false;
            }
        }
    }
}