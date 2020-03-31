// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System.Collections.Generic;
using UnityEngine;

namespace SOFlow.Data.Events
{
    public class GameEventReactorSet : MonoBehaviour
    {
        /// <summary>
        ///     The player events to listen for.
        /// </summary>
        public List<GameEventReactor> Events = new List<GameEventReactor>();

        private void Awake()
        {
            foreach(GameEventReactor @event in Events) @event.ActivateReactor(gameObject);
        }

        private void OnDestroy()
        {
            foreach(GameEventReactor @event in Events) @event.DeactivateReactor();
        }

#if UNITY_EDITOR
        /// <summary>
        ///     Adds a Game Event Reactor Set to the scene.
        /// </summary>
        [UnityEditor.MenuItem("GameObject/SOFlow/Events/Add Game Event Reactor Set", false, 10)]
        public static void AddComponentToScene()
        {
            GameObject _gameObject = new GameObject("Game Event Reactor Set", typeof(GameEventReactorSet));

            if(UnityEditor.Selection.activeTransform != null)
            {
                _gameObject.transform.SetParent(UnityEditor.Selection.activeTransform);
            }

            UnityEditor.Selection.activeGameObject = _gameObject;
        }
#endif
    }
}