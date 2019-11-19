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
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.ObjectPool.Master
{
    [CustomEditor(typeof(MasterPool))]
    public class MasterPoolEditor : Editor
    {
        #region VARIABLES

        /// <summary>
        /// Master Pool Reference.
        /// </summary>
        private MasterPool _target;

        #endregion


        #region UNITY METHODS

        private void OnEnable()
        {
            _target = target as MasterPool;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(15f);

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.LabelField("Master Pool Information", EditorStyles.helpBox);

            if (_target.Pool.Keys.Count > 0)
            {
                foreach (KeyValuePair<int, List<GameObject>> poolInfo in _target.Pool)
                {
                    EditorGUILayout
                        .LabelField($"Pool Identity: {poolInfo.Key} : #Pooled Objects: {poolInfo.Value.Count}",
                                    EditorStyles.helpBox);
                }
            }


            EditorGUILayout.EndVertical();
        }

        #endregion
    }
}
#endif