/*
 *
 * Created By: Ubaidullah Effendi-Emjedi, Uee
 * Alias: Uee
 * Modified By:
 *
 * Last Modified: 09 November 2019
 *
 */

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.ObjectPool.Single
{
    [CustomEditor(typeof(SinglePool))]
    public class SinglePoolEditor : Editor
    {
        #region VARIABLES

        /// <summary>
        /// Single Pool Reference.
        /// </summary>
        private SinglePool _target;

        #endregion


        #region UNITY METHODS

        private void OnEnable()
        {
            _target = target as SinglePool;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(15f);

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.LabelField("Single Pool Information", EditorStyles.helpBox);

            if (_target.Pool.Count > 0)
            {
                EditorGUILayout
                    .LabelField($"Pooled Item: {_target.Prefab.name} : #Pooled Objects: {_target.Pool.Count}",
                                EditorStyles.helpBox);
            }


            EditorGUILayout.EndVertical();
        }

        #endregion
    }
}
#endif