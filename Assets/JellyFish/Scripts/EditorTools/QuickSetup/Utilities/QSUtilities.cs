/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;
using SimpleJSON;
using SOFlow.Extensions;
#if UNITY_EDITOR
using UnityEditor;

namespace JellyFish.EditorTools.QuickSetup.Utilities
{
    public static class QSUtilities
    {
        #region METHODS

        /// <summary>
        /// Deserialize and Create an Object from a Json Settings file.
        /// </summary>
        public static void CreateGameObject()
        {
            Object settings = Selection.activeObject;

            if (settings != null)
            {
                if (settings.GetType() == typeof(TextAsset))
                {
                    string path = AssetDatabase.GetAssetPath(settings);

                    if (Path.GetExtension(path) == ".json")
                    {
                        DeserializeSettingsFile(path);
                    }
                    else
                    {
                        path = EditorUtility.OpenFilePanel("Setting File", Application.dataPath, "json");
                        DeserializeSettingsFile(path);
                    }
                }
                else
                {
                    string path = EditorUtility.OpenFilePanel("Setting File", Application.dataPath, "json");
                    DeserializeSettingsFile(path);
                }
            }
            else
            {
                string path = EditorUtility.OpenFilePanel("Setting File", Application.dataPath, "json");
                DeserializeSettingsFile(path);
            }
        }

        /// <summary>
        /// Deserialize a Json Settings File to an Object.
        /// </summary>
        /// <param name="path"></param>
        private static void DeserializeSettingsFile(string path)
        {
            string data = File.ReadAllText(path);
            JSONObject settings = (JSONObject) JSON.Parse(data);

            GameObject gameObject = null;

            foreach (KeyValuePair<string, JSONNode> valuePair in settings)
            {
                gameObject = new GameObject(valuePair.Key);

                foreach (KeyValuePair<string, JSONNode> nestedValuePair in valuePair.Value)
                {
                    CreateObjectWithComponents(nestedValuePair.Value.AsObject, nestedValuePair.Key)
                        .SetParent(gameObject);
                }
            }
        }

        /// <summary>
        /// Create Child Objects and accompanying Components from Nested Json Objects.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="name"></param>
        private static GameObject CreateObjectWithComponents(JSONObject settings, string name)
        {
            GameObject parent = new GameObject(name);

            try
            {
                foreach (KeyValuePair<string, JSONNode> valuePair in settings)
                {
                    if (valuePair.Key == "Components")
                    {
                        foreach (JSONNode node in valuePair.Value.AsArray)
                        {
                            string typeString = node;

                            Debug.Log(typeString);

                            if (typeString.Contains("UnityEngine."))
                            {
                                typeString = typeString.Replace("UnityEngine.", "");
                                Debug.Log(typeString);
                            }

                            Type type = TypeExtensions.GetInstanceType(typeString);

                            if (type != null)
                            {
                                parent.AddComponent(type);
                            }
                            else
                            {
                                Debug.LogError("Type Instance Not Found!");
                            }
                        }
                    }
                    else if (valuePair.Key == "GameObject")
                    {
                        GameObject child = new GameObject(valuePair.Value);
                        child.SetParent(parent);
                    }
                    else
                    {
                        CreateObjectWithComponents(valuePair.Value.AsObject, valuePair.Key).SetParent(parent);
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.Log(exception.ToString());
            }

            return parent;
        }

        #endregion
    }
}
#endif