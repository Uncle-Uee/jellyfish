// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System;
using System.IO;
using SFB;
using UnityEngine;

namespace SOFlow.Utilities
{
    public static class ScriptableObjectUtilities
    {
        /// <summary>
        ///     Saves the data of this scriptable object to a json file.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="saveDialogTitle"></param>
        /// <param name="defaultFileName"></param>
        /// <param name="OnSaveSuccess"></param>
        /// <param name="OnSaveFailure"></param>
        public static void SaveJSON(this ScriptableObject target,                       string saveDialogTitle = "Save",
                                    string                defaultFileName = "New Data", Action OnSaveSuccess   = null,
                                    Action                OnSaveFailure   = null)
        {
            string savePath = StandaloneFileBrowser.SaveFilePanel(saveDialogTitle,
                                                                  Environment.GetFolderPath(Environment.SpecialFolder
                                                                                                       .Desktop),
                                                                  defaultFileName, "json");

            if(!string.IsNullOrEmpty(savePath))
            {
                string jsonData = JsonUtility.ToJson(target);

                using(StreamWriter settingsFile = new StreamWriter(savePath, false))
                {
                    try
                    {
                        settingsFile.Write(jsonData);
                        OnSaveSuccess?.Invoke();
                    }
                    catch(IOException e)
                    {
                        Debug.LogError($"Failed to save {target.name} data.\n{e.Message}");
                        OnSaveFailure?.Invoke();
                    }
                    finally
                    {
                        settingsFile.Close();
                    }
                }
            }
        }

        /// <summary>
        ///     Saves the data of this scriptable object to a json file.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="filePath"></param>
        /// <param name="OnSaveSuccess"></param>
        /// <param name="OnSaveFailure"></param>
        /// <param name="extension"></param>
        public static void SaveJSONDirectly(this ScriptableObject target, string filePath,
                                            Action                OnSaveSuccess = null,
                                            Action                OnSaveFailure = null, string extension = "json")
        {
            string savePath = $"{filePath}.{extension}";

            if(!string.IsNullOrEmpty(savePath))
            {
                string jsonData = JsonUtility.ToJson(target);

                using(StreamWriter settingsFile = new StreamWriter(savePath, false))
                {
                    try
                    {
                        settingsFile.Write(jsonData);
                        OnSaveSuccess?.Invoke();
                    }
                    catch(IOException e)
                    {
                        Debug.LogError($"Failed to save {target.name} data.\n{e.Message}");
                        OnSaveFailure?.Invoke();
                    }
                    finally
                    {
                        settingsFile.Close();
                    }
                }
            }
        }

        /// <summary>
        ///     Loads scriptable object data from a json file.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="loadDialogTitle"></param>
        /// <param name="OnLoadSuccess"></param>
        /// <param name="OnLoadFailure"></param>
        public static void LoadJSON(this ScriptableObject target,               string loadDialogTitle = "Load",
                                    Action                OnLoadSuccess = null, Action OnLoadFailure   = null)
        {
            string[] loadPaths = StandaloneFileBrowser.OpenFilePanel(loadDialogTitle,
                                                                     Environment.GetFolderPath(Environment.SpecialFolder
                                                                                                          .Desktop),
                                                                     "json", false);

            foreach(string selectedFile in loadPaths)
            {
                using(StreamReader loadedFile = new StreamReader(File.OpenRead(selectedFile)))
                {
                    try
                    {
                        JsonUtility.FromJsonOverwrite(loadedFile.ReadToEnd(), target);
                        OnLoadSuccess?.Invoke();
                    }
                    catch(Exception e)
                    {
                        Debug.LogError($"{target.name} data failed to load.\n{e.Message}");
                        OnLoadFailure?.Invoke();
                    }
                }

                break; // Only a single path is selected, but break the loop to be sure only one file is loaded.
            }
        }

        /// <summary>
        ///     Loads scriptable object data from a json file.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="filePath"></param>
        /// <param name="extension"></param>
        /// <param name="OnLoadSuccess"></param>
        /// <param name="OnLoadFailure"></param>
        public static void LoadJSONDirectly(this ScriptableObject target, string filePath,
                                            string                extension     = "json",
                                            Action                OnLoadSuccess = null, Action OnLoadFailure = null)
        {
            string loadPath = $"{filePath}.{extension}";

            using(StreamReader loadedFile = new StreamReader(File.OpenRead(loadPath)))
            {
                try
                {
                    JsonUtility.FromJsonOverwrite(loadedFile.ReadToEnd(), target);
                    OnLoadSuccess?.Invoke();
                }
                catch(Exception e)
                {
                    Debug.LogError($"{target.name} data failed to load.\n{e.Message}");
                    OnLoadFailure?.Invoke();
                }
            }
        }
    }
}