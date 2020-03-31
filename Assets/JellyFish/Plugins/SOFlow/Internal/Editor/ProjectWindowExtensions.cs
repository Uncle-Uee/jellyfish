#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace SOFlow.Internal
{
    [InitializeOnLoad]
    public static class ProjectWindowExtensions
    {
        static ProjectWindowExtensions()
        {
            EditorApplication.projectWindowItemOnGUI -= OnProjectWindowItemGUI;
            EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemGUI;
        }

        /// <summary>
        /// Draws an asterisk before a Scriptable Object name if it has unsaved changes.
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="rect"></param>
        private static void OnProjectWindowItemGUI(string guid, Rect rect)
        {
            string itemPath = AssetDatabase.GUIDToAssetPath(guid);
            ScriptableObject asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(itemPath);

            if(asset && EditorUtility.IsDirty(asset))
            {
                rect.x -= 10f;
                rect.width = 10f;
                GUI.Label(rect, "*");
            }
        }
    }
}
#endif