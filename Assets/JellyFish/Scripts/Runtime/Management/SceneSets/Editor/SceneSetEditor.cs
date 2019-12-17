/**
 * Created by: Kearan Petersen
 * Blog: https://www.blumalice.wordpress.com
 * LinkedIn: https://www.linkedin.com/in/kearan-petersen/
 */

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;


// ReSharper disable once CheckNamespace
namespace JellyFish.Management.SceneSets
{
    [CustomEditor(typeof(SceneSet))]
    [CanEditMultipleObjects]
    public class SceneSetEditor : Editor
    {
        /// <summary>
        /// Field used to capture the input scene into the scene set.
        /// </summary>
        private Object _captureScene;

        /// <summary>
        /// The scene set target.
        /// </summary>
        private SceneSet _target;

        private void OnEnable()
        {
            _target = (SceneSet) target;
        }

        public override void OnInspectorGUI()
        {
            DrawSceneSet();
            GUILayout.Space(25f);

            if (_target.SetScenes.Count > 0)
            {
                GUILayout.Space(10f);
                DrawLoadSceneSetButton();
            }

            if (GUI.changed)
            {
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssets();
            }
        }

        /// <summary>
        /// Draws the scene set.
        /// </summary>
        private void DrawSceneSet()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("SetScenes"), true);
        }

        /// <summary>
        /// Draws the load scene set button.
        /// </summary>
        private void DrawLoadSceneSetButton()
        {
            if (GUILayout.Button("Load Scene Set"))
            {
                if (EditorUtility.DisplayDialog("Load Scene Set",
                                                "Replacing currently opens scenes with Scene Set.\nAre you sure?",
                                                "Load", "Cancel"))
                {
                    _target.LoadSceneSet(false);
                }
            }

            if (GUILayout.Button("Load Scene Set (Keep Current Scenes)"))
            {
                _target.LoadSceneSet(true);
            }

            if (GUILayout.Button("Unload Scene Set"))
            {
                _target.UnloadSceneSet();
            }
        }
    }
}
#endif