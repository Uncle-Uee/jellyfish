// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System;
using System.Collections.Generic;
using SOFlow.Extensions;
using UltEvents;
using UnityEngine;

namespace SOFlow.Data.Events
{
    [Serializable]
    public class GameEventReactor : IEventListener
    {
        /// <summary>
        ///     The game event to react to.
        /// </summary>
        public GameEvent Event;

        /// <summary>
        ///     The game object associated with this reactor.
        /// </summary>
        [HideInInspector]
        public GameObject GameObject;

        /// <summary>
        ///     The response to the game event.
        /// </summary>
        public UltEvent Response = new UltEvent();

        /// <inheritdoc />
        public void OnEventRaised(GameEvent raisedEvent)
        {
            Response.Invoke();
        }

        /// <inheritdoc />
        public GameObject GetGameObject()
        {
            return GameObject;
        }

        /// <inheritdoc />
        public Type GetObjectType()
        {
            return GetType();
        }

        /// <inheritdoc />
        public List<GameEvent> GetEvents()
        {
            return new List<GameEvent>
                   {
                       Event
                   };
        }

        /// <summary>
        ///     Activates the reactor.
        /// </summary>
        public void ActivateReactor(GameObject gameObject)
        {
            GameObject = gameObject;

            if(Event == null)
            {
                Debug.LogWarning($"[Game Event Reactor] No event supplied for reactor at : \n{gameObject.transform.GetPath()}");

                return;
            }

            Event.RegisterListener(this);
        }

        /// <summary>
        ///     Deactivates the reactor.
        /// </summary>
        public void DeactivateReactor()
        {
            Event.DeregisterListener(this);
        }
    }
}