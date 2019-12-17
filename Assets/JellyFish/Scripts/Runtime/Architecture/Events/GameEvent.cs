/**
 * Created by: Kearan Petersen
 * Blog: https://www.blumalice.wordpress.com
 * LinkedIn: https://www.linkedin.com/in/kearan-petersen/
 */


using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.Events
{
    [CreateAssetMenu(menuName = "JellyFish/Events/Game Event")]
    public class GameEvent : ScriptableObject
    {
        /// <summary>
        /// All listeners registered to this game event.
        /// </summary>
        private readonly List<GameEventListener> _listeners = new List<GameEventListener>();

        /// <summary>
        /// Notifies all registered listeners to invoke their events.
        /// </summary>
        public void Raise()
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised();
            }
        }

        /// <summary>
        /// Registers a listener to this game event.
        /// </summary>
        /// <param name="listener"></param>
        /// <param name="registerLast"></param>
        public void RegisterListener(GameEventListener listener, bool registerLast = false)
        {
            if (registerLast)
            {
                _listeners.Insert(0, listener);
            }
            else
            {
                _listeners.Add(listener);
            }
        }

        /// <summary>
        /// Deregisters a listener from this game event.
        /// </summary>
        /// <param name="listener"></param>
        public void DeregisterListener(GameEventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}