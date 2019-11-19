/*
 *
 * Created By: Kearan Peterson
 * Alias: Uee
 * Modified By: Ubaidullah Effendi-Emjedi, Uee
 *
 * Last Modified: 09 November 2019
 *
 */

using System.Collections;
using System.Collections.Generic;
using JellyFish.Data.Primitive;
using UltEvents;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

// ReSharper disable once CheckNamespace
namespace JellyFish.Events
{
    public class GameEventListener : MonoBehaviour
    {
        /// <summary>
        /// The list of conditions for this event.
        /// </summary>
        public List<IntReference> Conditions = new List<IntReference>();

        /// <summary>
        /// Indicates whether debug logs should be performed when events are raised.
        /// </summary>
        public bool Debug;

        /// <summary>
        /// The game event to listen for.
        /// </summary>
        public GameEvent Event;

        /// <summary>
        /// The event listener order.
        /// </summary>
        public int EventListenerOrder;

        /// <summary>
        /// Indicates whether condition checks should be inverted.
        /// </summary>
        public bool InvertConditions;

        /// <summary>
        /// Indicates whether this listener should be prioritized before other listeners when events are raised.
        /// </summary>
        [FormerlySerializedAs("PrioritizeListener")]
        public bool RegisterLast;

        /// <summary>
        /// Indicates whether the event listener should register to the given event on Awake.
        /// </summary>
        public bool RegisterOnAwake;

        /// <summary>
        /// The response to the game event.
        /// </summary>
        public UltEvent Response;

        private void Awake()
        {
            if (RegisterOnAwake)
            {
                Event.RegisterListener(this);
            }
        }

        private void OnEnable()
        {
            if (!RegisterOnAwake)
            {
                StartCoroutine(RegisterEvent());
            }
        }

        private void OnDisable()
        {
            if (!RegisterOnAwake)
            {
                StopAllCoroutines();

                Event.DeregisterListener(this);
            }
        }

        private void OnDestroy()
        {
            if (RegisterOnAwake)
            {
                StopAllCoroutines();

                Event.DeregisterListener(this);
            }
        }

        /// <summary>
        /// Registers the event.
        /// </summary>
        /// <returns></returns>
        private IEnumerator RegisterEvent()
        {
            yield return new WaitForSeconds(EventListenerOrder);

            Event.RegisterListener(this, RegisterLast);
        }

        /// <summary>
        /// Event callback.
        /// </summary>
        public void OnEventRaised()
        {
            bool conditionsMet = true;

            foreach (IntReference condition in Conditions)
            {
                if (InvertConditions ? condition.Value != 0 : condition.Value != 1)
                {
                    conditionsMet = false;

                    break;
                }
            }

            if (conditionsMet)
            {
                Response?.Invoke();

                if (Debug)
                {
                    UnityEngine.Debug.Log(Event.name);
                }
            }
        }
    }
}