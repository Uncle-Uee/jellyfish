/**
 * Created by: Kearan Petersen
 * Blog: https://www.blumalice.wordpress.com
 * LinkedIn: https://www.linkedin.com/in/kearan-petersen/
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