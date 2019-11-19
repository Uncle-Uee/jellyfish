/*
 *
 * Created By: Kearan Peterson
 * Alias: Uee
 * Modified By: Ubaidullah Effendi-Emjedi, Uee
 *
 * Last Modified: 09 November 2019
 *
 */

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.Events
{
#if UNITY_EDITOR
    [CustomEditor(typeof(GameEvent))]
    public class GameEventEditor : Editor
    {
        #region VARIABLES

        private GameEvent _target;

        #endregion


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            _target     = target as GameEvent;
            GUI.enabled = Application.isPlaying;

            if (GUILayout.Button("Raise Event"))
            {
                _target?.Raise();
            }
        }
    }
#endif
}