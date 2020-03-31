// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System.Collections.Generic;
using UnityEngine;

namespace JellyFish.PlayerInput
{
    public class GameInputListener : MonoBehaviour
    {
        /// <summary>
        ///     Enable to automatically check all game input on this listener.
        /// </summary>
        public bool AutoCheckGameInput = true;

        /// <summary>
        ///     The input this component is listening for.
        /// </summary>
        public List<GameInput> Input = new List<GameInput>();

        private void Update()
        {
            if(AutoCheckGameInput) CheckGameInput();
        }

        /// <summary>
        ///     Checks the current state of all game input on this listener.
        /// </summary>
        public void CheckGameInput()
        {
            foreach(GameInput input in Input) input.CheckInputState();
        }

#if UNITY_EDITOR
        /// <summary>
        ///     Adds a Game Input Listener to the scene.
        /// </summary>
        [UnityEditor.MenuItem("GameObject/SOFlow/Input/Add Game Input Listener", false, 10)]
        public static void AddComponentToScene()
        {
            GameObject _gameObject = new GameObject("Game Input Listener", typeof(GameInputListener));

            if(UnityEditor.Selection.activeTransform != null)
            {
                _gameObject.transform.SetParent(UnityEditor.Selection.activeTransform);
            }

            UnityEditor.Selection.activeGameObject = _gameObject;
        }
#endif
    }
}