// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System;
using System.Collections.Generic;
using SOFlow.Data.Primitives;
using SOFlow.Extensions;
using UltEvents;
using UnityAsync;
using UnityEngine;

namespace SOFlow.Data.Events
{
    public partial class GameEventListener : MonoBehaviour, IEventListener
    {
        /// <summary>
        /// The delay before the event response is invoked.
        /// </summary>
        public FloatField ResponseDelay = new FloatField();
        
        /// <summary>
        ///     The game event to listen for.
        /// </summary>
        public List<GameEvent> Events = new List<GameEvent>();

        /// <summary>
        ///     The event listener order.
        /// </summary>
        public FloatField EventListenerOrder = new FloatField();

        /// <summary>
        ///     Indicates whether this listener should be prioritized after other listeners when events are raised.
        /// </summary>
        public BoolField RegisterLast = new BoolField();

        /// <summary>
        ///     Indicates whether the event listener should register to the given event on Awake.
        /// </summary>
        public BoolField RegisterOnAwake = new BoolField();

        /// <summary>
        ///     Indicates whether this event should be debugged.
        /// </summary>
        public BoolField Debug = new BoolField();

        /// <summary>
        ///     The list of conditions for this event.
        /// </summary>
        public List<EventCondition> Conditions = new List<EventCondition>();

        /// <summary>
        ///     The response to the game event.
        /// </summary>
        public UltEvent Response = new UltEvent();

        /// <summary>
        ///     The game object reference.
        /// </summary>
        private GameObject _gameObjectReference;

        /// <inheritdoc />
        public async void OnEventRaised(GameEvent raisedEvent)
        {
            bool conditionsMet = true;

            foreach(EventCondition condition in Conditions)
                if(!condition.Evaluate())
                {
                    conditionsMet = false;

                    break;
                }

            if(conditionsMet)
            {
                if(ResponseDelay.Value > 0f)
                {
                    await Await.Seconds(ResponseDelay);
                }

                Response.Invoke();

                if(Debug) UnityEngine.Debug.Log($"|Game Event Listener| Responding to event : \n{raisedEvent.name}");
            }
        }

        /// <inheritdoc />
        public GameObject GetGameObject()
        {
            return _gameObjectReference;
        }

        /// <inheritdoc />
        public Type GetObjectType()
        {
            return GetType();
        }

        /// <inheritdoc />
        public List<GameEvent> GetEvents()
        {
            return Events;
        }

        private void Awake()
        {
            _gameObjectReference = gameObject;

            if(!Application.isPlaying) return;

            if(RegisterOnAwake)
                foreach(GameEvent @event in Events)
                {
                    if(@event == null)
                    {
                        UnityEngine.Debug
                                   .LogWarning($"[Game Event Listener Set] No event supplied for listener at : \n{transform.GetPath()}");

                        continue;
                    }

                    @event.RegisterListener(this, RegisterLast);
                }
        }

        private async void OnEnable()
        {
            if(!Application.isPlaying) return;

            if(!RegisterOnAwake)
            {
                await Await.Seconds(EventListenerOrder);

                foreach(GameEvent @event in Events)
                {
                    if(@event == null)
                    {
                        UnityEngine.Debug
                                   .LogWarning($"[Game Event Listener Set] No event supplied for listener at : \n{transform.GetPath()}");

                        continue;
                    }

                    @event.RegisterListener(this, RegisterLast);
                }
            }
        }

        private void OnDisable()
        {
            if(!Application.isPlaying) return;

            if(!RegisterOnAwake)
            {
                StopAllCoroutines();

                foreach(GameEvent @event in Events) @event.DeregisterListener(this);
            }
        }

        private void OnDestroy()
        {
            if(!Application.isPlaying) return;

            if(RegisterOnAwake)
            {
                StopAllCoroutines();

                foreach(GameEvent @event in Events) @event.DeregisterListener(this);
            }
        }
    }
}