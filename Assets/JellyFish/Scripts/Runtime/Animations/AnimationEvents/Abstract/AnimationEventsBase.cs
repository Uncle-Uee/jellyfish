using UltEvents;
using UnityEngine;

namespace JellyFish.Animations.Events
{
    public abstract class AnimationEventsBase : MonoBehaviour
    {
        #region VARIABLES

        /// <summary>
        /// Starting Animation Event.
        /// </summary>
        [Header("Events")]
        public UltEvent OnAnimationStart = new UltEvent();

        /// <summary>
        /// Ending Animation Event.
        /// </summary>
        public UltEvent OnAnimationEnd = new UltEvent();

        #endregion

        #region METHODS

        /// <summary>
        /// Trigger Starting Animation Event Function
        /// </summary>
        public virtual void OnStartingAnimationEvent()
        {
            OnAnimationStart.Invoke();
        }

        /// <summary>
        /// Trigger Ending Animation Event Function.
        /// </summary>
        public virtual void OnEndingAnimationEvent()
        {
            OnAnimationEnd.Invoke();
        }

        /// <summary>
        /// Disable this Object at the End of the Animation
        /// </summary>
        public virtual void DisableAtAnimationEnd()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Destroy this Object at the End of the Animation
        /// </summary>
        public virtual void DestroyAtAnimationEnd()
        {
            Destroy(gameObject);
        }

        #endregion
    }
}