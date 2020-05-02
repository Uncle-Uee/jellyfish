/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using System.IO;
using UnityEngine;
using JellyFish.Editor.Utilities;
#if UNITY_EDITOR
using UnityEditor;

namespace JellyFish.Editor.Tools.TextFiles
{
    public static class SimpleTextFiles
    {
        #region METHODS

        /// <summary>
        /// Create a File using UnityEditor Utilities Save File Panel.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="defaultName"></param>
        /// <param name="extension"></param>
        private static void CreateFilePanel(string title, string defaultName, string extension)
        {
            Object folder = Selection.activeObject;

            if (folder != null)
            {
                string directory = AssetDatabase.GetAssetPath(folder);
                string path      = "";

                if (Directory.Exists(directory))
                {
                    path = EditorUtility.SaveFilePanel(title, directory, defaultName, extension);
                    SimpleFileCreator.CreateFileAt(path, filepath => { File.CreateText(filepath); });
                }
                else
                {
                    Directory.CreateDirectory(directory);
                    path = EditorUtility.SaveFilePanel(title, directory, defaultName, extension);
                    SimpleFileCreator.CreateFileAt(path, filepath => { File.CreateText(filepath); });
                }
            }
            else
            {
                string path = EditorUtility.SaveFilePanelInProject(title, defaultName, extension, "");
                SimpleFileCreator.CreateFileAt(path, filepath => { File.Create(filepath); });
            }

            AssetDatabase.Refresh();
        }


        /// <summary>
        /// Create Text File.
        /// </summary>
        [MenuItem("Assets/Create/JellyFish/Text File/Text Asset", priority = 10)]
        public static void CreateTextFile()
        {
            CreateFilePanel("Text File", "text-file", "txt");
        }


        /// <summary>
        /// Create Json File.
        /// </summary>
        [MenuItem("Assets/Create/JellyFish/Text File/Json File", priority = 10)]
        public static void CreateJsonFile()
        {
            CreateFilePanel("Json File", "json-file", "json");
        }

        #endregion
    }
}
#endif