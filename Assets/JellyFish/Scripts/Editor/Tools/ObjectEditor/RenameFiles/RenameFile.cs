#if UNITY_EDITOR
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace JellyFish.Editor.Tools.ObjectEditor
{
    public class RenameFile : EditorWindow
    {
        #region VARIABLES

        string                  _filename = "";
        private static Object[] _objects;

        #endregion

        #region UNITY EDITOR WINDOW

        /// <summary>
        /// RenameFile Window Instance
        /// </summary>
        private static RenameFile _window;

        private static void OpenWindow()
        {
            _window         = GetWindow<RenameFile>("Filename");
            _window.minSize = new Vector2(320, 180);
            _window.Show();
        }

        #endregion

        #region UNITY METHODS

        public void OnGUI()
        {
            _filename = EditorGUILayout.TextField("New Filename:", _filename);

            if (GUILayout.Button("Rename Files"))
            {
                RenameAll(_filename, _objects);
                _window.Close();
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Open Up Rename File Window.
        /// </summary>
        [MenuItem("Assets/Rename Selected Files", priority = 30)]
        public static void ContextMenuOption()
        {
            _objects = Selection.objects;
            OpenWindow();
        }

        /// <summary>
        /// Rename All Selected Objects With a Common Name
        /// </summary>
        private void RenameAll(string filename, Object[] objects)
        {
            int length = objects.Length;

            for (int i = 0; i < length; i++)
            {
                string path        = AssetDatabase.GetAssetPath(objects[i]);
                string oldFilename = Path.GetFileName(path);
                string extension   = Path.GetExtension(path).Replace(".", "");

                string[] metadataPaths = Directory.GetFiles(path.Replace(oldFilename, ""), "*.meta");
                string   metadataPath  = metadataPaths.FirstOrDefault(element => element.Contains(oldFilename));
                string   newFilename   = $"{filename}-{i}.{extension}";

                // Rename the Selected Files Corresponding Metadata 
                File.Move(metadataPath, metadataPath.Replace(oldFilename, newFilename));
                // Rename the Selected File
                File.Move(path, path.Replace(oldFilename, newFilename));
            }

            AssetDatabase.Refresh();
        }

        #endregion
    }
}
#endif