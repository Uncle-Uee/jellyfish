#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace JellyFish.EditorTools.ObjectEditor
{
    public partial class RenameFile
    {
        #region VARIABLES

        /// <summary>
        /// New Filename
        /// </summary>
        private string filename = "";

        /// <summary>
        /// List of Selected Objects.
        /// </summary>
        private static Object[] _objects;

        #endregion

        #region UNITY METHODS

        public void OnGUI()
        {
            filename = EditorGUILayout.TextField("New Filename:", filename);

            if (GUILayout.Button("Rename Files"))
            {
                RenameAll(filename, _objects);
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
        private void RenameAll(string newFilename, Object[] objects)
        {
            int length = objects.Length;

            string[] metadataPaths = null;

            for (int i = 0; i < length; i++)
            {
                string path = AssetDatabase.GetAssetPath(objects[i]);
                string oldFilename = Path.GetFileName(path);
                string extension = Path.GetExtension(path).Replace(".", "");

                if (metadataPaths == null)
                {
                    metadataPaths = Directory.GetFiles(path.Replace(oldFilename, ""), "*.meta");
                }

                string metadataPath = metadataPaths[i];

                if (newFilename == string.Empty)
                {
                    Debug.Log("No Filename");
                    return;
                }

                // Append "i" index if there is more than 1 File Selected.
                newFilename = length == 1 ? $"{newFilename}.{extension}" : $"{newFilename}-{i}.{extension}";
                
                // Rename the Selected File
                File.Move(path, path.Replace(oldFilename, newFilename));

                // Rename the Selected Files Corresponding Metadata 
                File.Move(metadataPath, metadataPath.Replace(oldFilename, $"{newFilename}-{i}.{extension}"));
            }

            AssetDatabase.Refresh();
        }

        #endregion
    }
}
#endif