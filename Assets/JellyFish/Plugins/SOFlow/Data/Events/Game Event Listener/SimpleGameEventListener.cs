// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System;
using System.Collections.Generic;
using SOFlow.Data.Primitives;
using UltEvents;
using UnityAsync;
using UnityEngine;

namespace SOFlow.Data.Events
{
    public class SimpleGameEventListener : MonoBehaviour, IEventListener
    {
        /// <summary>
        /// The delay before the event response is invoked.
        /// </summary>
        public FloatField ResponseDelay = new FloatField();
        
        /// <summary>
        /// The game event to listen for.
        /// </summary>
        public GameEvent Event;

        /// <summary>
        /// The response to the game event.
        /// </summary>
        public UltEvent Response = new UltEvent();

        /// <summary>
        ///     The game object reference.
        /// </summary>
        private GameObject _gameObjectReference;
        
        /// <summary>
        /// The event list cache.
        /// </summary>
        private readonly List<GameEvent> _eventListCache = new List<GameEvent>();

        /// <inheritdoc />
        public async void OnEventRaised(GameEvent raisedEvent)
        {
            if(ResponseDelay.Value > 0f)
            {
                await Await.Seconds(ResponseDelay);
            }

            Response.Invoke();
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
            return _eventListCache;
        }

        private void Awake()
        {
            _gameObjectReference = gameObject;
            _eventListCache.Add(Event);
        }

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.DeregisterListener(this);
        }
        
#if UNITY_EDITOR
        /// <summary>
        ///     Adds a Simple Game Event Listener to the scene.
        /// </summary>
        [UnityEditor.MenuItem("GameObject/SOFlow/Events/Add Simple Game Event Listener", false, 10)]
        public static void AddComponentToScene()
        {
            GameObject _gameObject = new GameObject("Simple Game Event Listener", typeof(SimpleGameEventListener));

            if(UnityEditor.Selection.activeTransform != null)
            {
                _gameObject.transform.SetParent(UnityEditor.Selection.activeTransform);
            }

            UnityEditor.Selection.activeGameObject = _gameObject;
        }
#endif
    }
}