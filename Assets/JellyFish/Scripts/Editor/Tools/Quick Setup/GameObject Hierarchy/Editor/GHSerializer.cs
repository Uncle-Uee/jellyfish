/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

#if UNITY_EDITOR
using System;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;
using UnityEditor;
using UnityEditor.Callbacks;

namespace JellyFish.Editor.Tools.QuickSetup
{
    /// <summary>
    /// Simple GameObject Hierarchy Serializer that can easily Serialize a selected GameObjects complete Hierarchy into a simple Json Format "GHJson"
    /// as well as Deserialize that Json data back into a complete GameObject Hierarchy.
    /// GHJson -> GameObject Hierarchy Json Format, is a made-up format. The data contained within the GHJson File is pure Json data.
    /// </summary>
    public static class GHSerializer
    {
        #region MAGIC METHOD

        /// <summary>
        /// Deserialize QSJson Dialog Panel Options.
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        [OnOpenAsset(0)]
        public static bool DeserializeQSJsonMagicMethod(int instanceId, int line)
        {
            Object _object = EditorUtility.InstanceIDToObject(instanceId);

            string path = AssetDatabase.GetAssetPath(_object);

            if (Path.GetExtension(path)?.ToLower() == ".ghjson")
            {
                // Creates Dialog
                int doThis = EditorUtility.DisplayDialogComplex("Create GameObject Hierarchy",
                                                                "Do you want to create your GameObject?",
                                                                "Create GameObject", "Cancel", "Open File");

                if (doThis == 0)
                {
                    string json = File.ReadAllText(path);
                    JsonSerializer.DeserializeTransformHierarchy(json);

                    return true;
                }

                if (doThis == 1)
                {
                    return true;
                }

                if (doThis == 2)
                {
                    return false;
                }
            }

            return false;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Serialize a Valid GameObject Hierarchy into a valid QuickSetup Json File.
        /// </summary>
        [MenuItem("GameObject/JellyFish/QuickSetup/GameObject/Serialize GameObject", false, 10)]
        public static void SerializeHierarchy()
        {
            try
            {
                Transform transform = Selection.activeTransform;

                if (transform)
                {
                    string path = EditorUtility.SaveFilePanel("Save GHJson File", Application.dataPath, $"{transform.name}", "ghjson");
                    string json = JsonSerializer.SerializeTransformHierarchy(transform, true);

                    File.WriteAllText(path, json);

                    Debug.Log("Serialization Completed 😎");
                }

                AssetDatabase.Refresh();
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString());
            }
        }

        /// <summary>
        /// Deserialize QuickSetup Json File into a Valid GameObject Hierarchy.
        /// </summary>
        [MenuItem("Assets/JellyFish/QuickSetup/GameObject/Deserialize GHJson", priority = 10)]
        public static void DeserializeHierarchy()
        {
            try
            {
                Object _object = Selection.activeObject;

                if (_object)
                {
                    string path = AssetDatabase.GetAssetPath(_object);

                    if (Path.GetExtension(path)?.ToLower() == ".ghjson")
                    {
                        string json = File.ReadAllText(path);
                        JsonSerializer.DeserializeTransformHierarchy(json);

                        Debug.Log("Deserialization Completed 😎");
                    }
                    else
                    {
                        path = EditorUtility.OpenFilePanel("Select GHJson File", Application.dataPath, "ghjson");
                        string json = File.ReadAllText(path);
                        JsonSerializer.DeserializeTransformHierarchy(json);
                        Debug.Log("Deserialization Completed 😎");
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString());
            }
        }

        #endregion
    }
}
#endif