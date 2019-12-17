/**
 * Created by: Kearan Petersen
 * Blog: https://www.blumalice.wordpress.com
 * LinkedIn: https://www.linkedin.com/in/kearan-petersen/
 */



using System.Collections;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.Events
{
    public class GameEventRaiser : MonoBehaviour
    {
        /// <summary>
        /// The game event to raise.
        /// </summary>
        public GameEvent Event;

        /// <summary>
        /// The time before the event is raised in seconds.
        /// </summary>
        public float EventWaitTime;

        private void Start()
        {
            RaiseEvent();
        }

        /// <summary>
        /// Raises the event.
        /// </summary>
        public void RaiseEvent()
        {
            StartCoroutine(nameof(OnRaiseEvent));
        }

        private IEnumerator OnRaiseEvent()
        {
            if (EventWaitTime > 0f)
            {
                yield return new WaitForSeconds(EventWaitTime);
            }

            Event?.Raise();
        }
    }
}