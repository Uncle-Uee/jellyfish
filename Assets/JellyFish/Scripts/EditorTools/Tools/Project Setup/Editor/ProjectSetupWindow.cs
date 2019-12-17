/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
#if UNITY_EDITOR
using UnityEditor.Callbacks;
#endif
using UnityEngine;

// ReSharper disable once CheckNamespace
#if UNITY_EDITOR
namespace JellyFish.EditorTools.ProjectSetup
{
    public class ProjectSetupWindow : EditorWindow
    {
        #region MAGIC METHOD

        /// <summary>
        ///     Possible to Create Folders when Clicking on a .json File.
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        [OnOpenAsset(0)]
        public static bool FolderCreationMagic(int instanceId, int line)
        {
            Object unityObject = EditorUtility.InstanceIDToObject(instanceId);

            if (unityObject.GetType() == typeof(TextAsset))
            {
                TextAsset json = unityObject as TextAsset;
                string path = AssetDatabase.GetAssetPath(json);

                if (Path.GetExtension(path)?.ToLower() == ".json")
                {
                    // Creates Dialog
                    int doThis = EditorUtility.DisplayDialogComplex("Create Folders",
                                                                    "Do you want to create your folders?",
                                                                    "Create Folders", "Cancel", "Open File");

                    if (doThis == 0)
                    {
                        CreateFolderStructure(json.text);

                        return true;
                    }

                    if (doThis == 1) return true;

                    if (doThis == 2) return false;
                }
            }

            return false;
        }

        #endregion

        #region EDITOR WINDOW

        private static ProjectSetupWindow _window;

        [MenuItem("JellyFish/Project Setup/Project Setup Editor")]
        public static void CreateWindow()
        {
            _window = GetWindow<ProjectSetupWindow>("Project Setup");
            _window.Show();
        }

        #endregion

        #region VARIABLES

        /// <summary>
        ///     Color of the Create Folder Structure Button
        /// </summary>
        private readonly Color _green = new Color(1f, 0.23f, 0.21f, 0.8f);

        /// <summary>
        ///     Json Folder Structure File.
        /// </summary>
        private TextAsset JsonFile { get; set; }

        /// <summary>
        ///     Root Folder of Project.
        /// </summary>
        private static string RootFolderPath => Application.dataPath;

        #endregion

        #region GUI

        private void OnGUI()
        {
            DrawProjectSetup();
        }


        /// <summary>
        ///     Draw Simple Project Setup View.
        /// </summary>
        private void DrawProjectSetup()
        {
            EditorGUILayout.BeginVertical();

            JsonFile = (TextAsset) EditorGUILayout.ObjectField(new GUIContent("Folder Structure Json File",
                                                                              "Drag and Drop the Structure File"),
                                                               JsonFile, typeof(TextAsset), false);

            if (CreateFolders())
                if (JsonFile != null)
                    CreateFolderStructure(JsonFile.text);

            EditorGUILayout.EndVertical();
        }

        #endregion

        #region HELPER METHODS

        /// <summary>
        ///     Button to Create Folders Button.
        /// </summary>
        /// <returns></returns>
        private bool CreateFolders()
        {
            Color previousColor = GUI.backgroundColor;
            GUI.backgroundColor = _green;

            bool clicked = GUILayout.Button("Create Folders");

            GUI.backgroundColor = previousColor;

            return clicked;
        }


        /// <summary>
        ///     Create the Folder Structure from the Json Data.
        /// </summary>
        /// <param name="jsonData"></param>
        private static void CreateFolderStructure(string jsonData)
        {
            // Read Json File.
            JsonTextReader reader = new JsonTextReader(new StringReader(jsonData));

            List<string> assetSubFolders = new List<string>
            {
                "", RootFolderPath
            };

            string subFolder = "";
            string path = "";

            // Level Depth of Sub Folders.
            int level = 0;

            // While there are Still Nested Directories to Create
            while (reader.Read())
                if (reader.TokenType == JsonToken.PropertyName && reader.Value != null)
                {
                    // Create Sub Folders of the Assets Folder namely Resources, Scripts, etc
                    if (level == 1)
                    {
                        assetSubFolders.Add(assetSubFolders[level] + $"/{reader.Value}");
                        CreateDirectory(assetSubFolders[assetSubFolders.Count - 1]);
                    }

                    // Create Sub Folders Within the Resources, Scripts Folders, etc.
                    else if (level == 2)
                    {
                        subFolder = $"/{reader.Value}";
                        CreateDirectory(assetSubFolders[assetSubFolders.Count - 1] + subFolder);
                    }

                    // Create Nested Sub Folders within the above Created Sub Folders
                    else if (level == 3)
                    {
                        path = assetSubFolders[assetSubFolders.Count - 1] + subFolder + $"/{reader.Value}";
                        CreateDirectory(path);
                    }

                    // Handle Every other Level Depth of Nested Sub Folder Creation.
                    else
                    {
                        level = CreateSubFolders(reader, path, $"/{reader.Value}", level);
                    }
                }

                else if (reader.TokenType == JsonToken.StartObject)
                {
                    //ToDo : Increase Folder Depth
                    level += 1;
                }

                else if (reader.TokenType == JsonToken.EndObject)
                {
                    //ToDo : Decrease Folder Depth
                    level -= 1;
                }

            AssetDatabase.Refresh();
        }

        /// <summary>
        ///     Create the Nested Levels of Sub Directories of Folders.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="path"></param>
        /// <param name="folder"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        private static int CreateSubFolders(JsonTextReader reader, string path, string folder, int level)
        {
            List<string> subFolders = new List<string>
            {
                path
            };

            string subFolder = "";

            // Attempt to Create Directory
            if (folder != "")
                CreateDirectory(path + folder);

            // While there are Still Nested Sub Folders to Create
            while (reader.Read())
                if (reader.TokenType == JsonToken.PropertyName && reader.Value != null)
                {
                    if (subFolders.Count >= 1)
                    {
                        // Create the Current Paths Sub Directories.
                        subFolder = $"/{reader.Value}";
                        CreateDirectory(subFolders[subFolders.Count - 1] + subFolder);
                    }
                    else
                    {
                        // Otherwise, the Current Folder has Sub Directories, so Create them.
                        CreateSubFolders(reader, subFolders[subFolders.Count - 1] + subFolder, $"/{reader.Value}",
                                         level);
                    }
                }
                else if (reader.TokenType == JsonToken.StartObject)
                {
                    //ToDo : Create Nested Sub Folders.
                    CreateSubFolders(reader, subFolders[subFolders.Count - 1] + subFolder, "", level);
                }

                else if (reader.TokenType == JsonToken.EndObject)
                {
                    //ToDo : Decrease Folder Depth
                    return level -= 1;
                }

            // ToDo : If there a No more Sub Folders to Create Return the Level Depth of the Sub Folders.
            return level;
        }

        /// <summary>
        ///     Create a Directory at a given Path.
        /// </summary>
        /// <param name="path"></param>
        private static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        #endregion
    }
}
#endif