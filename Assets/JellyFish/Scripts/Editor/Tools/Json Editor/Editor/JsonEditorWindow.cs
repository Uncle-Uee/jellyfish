/*
 *
 * Created By: Kearan Peterson
 * Alias: BluMalice
 * Modified By: Ubaidullah Effendi-Emjedi, Uee
 *
 * Last Modified: 09 November 2019
 *
 */

#if UNITY_EDITOR
using System;
using System.Globalization;
using System.IO;
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.Editor.Tools.JsonEditor
{
    public class JsonEditorWindow : EditorWindow
    {
        #region EDITOR WINDOW

        private static JsonEditorWindow _window;

        [MenuItem("JellyFish/Tools/Json Editor")]
        public static void CreateWindow()
        {
            _window = GetWindow<JsonEditorWindow>("Json Window");
            _window.Show();
        }

        #endregion

        #region VARIABLES

        /// <summary>
        ///     Scroll Editor Window.
        /// </summary>
        private Vector2 _scroll = Vector2.zero;

        private readonly Color blue   = new Color(0f, 0.5647058823529412f, 1f, 0.5f);
        private readonly Color red    = new Color(1f, 0f, 0.4313725490196078f, 0.8f);
        private readonly Color green  = new Color(0.2666666666666667f, 1f, 0.4627450980392157f, 0.8f);
        private readonly Color orange = new Color(1f, 0.4196078431372549f, 0.2392156862745098f, 0.8f);

        /// <summary>
        ///     Json Data Object.
        /// </summary>
        [NonSerialized]
        private SimpleJsonList _jsonObject = new SimpleJsonList();

        /// <summary>
        ///     Json Data String
        /// </summary>
        private string _json;

        #endregion

        #region GUI METHOD

        private void OnGUI()
        {
            _scroll = EditorGUILayout.BeginScrollView(_scroll, false, true);

            if (CreateJson())
            {
                string path =
                    EditorUtility.SaveFilePanel("Save Json File", Application.dataPath,
                                                "json-file", "json");

                if (path.Length != 0)
                {
                    File.WriteAllText(path, "{\n}");
                    AssetDatabase.Refresh();
                }
            }

            DrawSimpleJsonList(ref _jsonObject);

            if (SaveJson())
            {
                string path = EditorUtility.OpenFilePanel("Select Json File",
                                                          "Assets/Tools/ProjectSetup/Settings/", "json");

                _json = _jsonObject.ToJson();

                File.WriteAllText(path, _json);

                AssetDatabase.Refresh();
            }

            EditorGUILayout.EndScrollView();
        }

        #endregion

        #region EDITOR WINDOW METHODS

        /// <summary>
        ///     Draws a simple json list.
        /// </summary>
        private void DrawSimpleJsonList(ref SimpleJsonList jsonList)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            GUI.backgroundColor = blue;

            EditorGUILayout.BeginHorizontal();

            GUIStyle labelStyle = new GUIStyle(EditorStyles.label)
            {
                wordWrap = true
            };

            if (GUILayout.Button(jsonList.IsExpanded ? "↓" : "↑", GUILayout.Width(20f)))
            {
                jsonList.IsExpanded = !jsonList.IsExpanded;
            }

            EditorGUILayout.LabelField("Entry Type", labelStyle);
            EditorGUILayout.LabelField("Entry Key", labelStyle);
            EditorGUILayout.LabelField("Entry Value", labelStyle);

            EditorGUILayout.EndHorizontal();

            int removeEntryIndex = -1;

            if (jsonList.IsExpanded)
            {
                for (int i = 0; i < jsonList.Entries.Count; i++)
                {
                    SimpleJsonEntry entry = jsonList.Entries[i];
                    EditorGUILayout.BeginHorizontal();

                    entry.Type = (SimpleJsonEntry.JsonTypes) EditorGUILayout.EnumPopup(entry.Type);
                    entry.Key = EditorGUILayout.TextField(entry.Key);

                    switch (entry.Type)
                    {
                        case SimpleJsonEntry.JsonTypes.STRING:
                            entry.Value = EditorGUILayout.TextField(entry.Value);

                            if (DrawJsonEntryDeleteButton()) removeEntryIndex = i;

                            EditorGUILayout.EndHorizontal();

                            break;
                        case SimpleJsonEntry.JsonTypes.INT:
                            int intValue;
                            int.TryParse(entry.Value, out intValue);

                            entry.Value = EditorGUILayout.IntField(intValue).ToString();

                            if (DrawJsonEntryDeleteButton()) removeEntryIndex = i;

                            EditorGUILayout.EndHorizontal();

                            break;
                        case SimpleJsonEntry.JsonTypes.FLOAT:
                            float floatValue;
                            float.TryParse(entry.Value, out floatValue);

                            entry.Value = EditorGUILayout.FloatField(floatValue).ToString(CultureInfo.InvariantCulture);

                            if (DrawJsonEntryDeleteButton()) removeEntryIndex = i;

                            EditorGUILayout.EndHorizontal();

                            break;
                        case SimpleJsonEntry.JsonTypes.BOOL:
                            bool boolValue;
                            bool.TryParse(entry.Value, out boolValue);

                            entry.Value = EditorGUILayout.Toggle(boolValue).ToString();

                            if (DrawJsonEntryDeleteButton()) removeEntryIndex = i;

                            EditorGUILayout.EndHorizontal();

                            break;
                        case SimpleJsonEntry.JsonTypes.LIST:

                            if (DrawJsonEntryDeleteButton()) removeEntryIndex = i;

                            EditorGUILayout.EndHorizontal();

                            EditorGUI.indentLevel++;
                            DrawSimpleJsonList(ref entry.List);
                            EditorGUI.indentLevel--;

                            break;
                        default:
                            entry.Value = EditorGUILayout.TextField(entry.Value);

                            if (DrawJsonEntryDeleteButton()) removeEntryIndex = i;

                            EditorGUILayout.EndHorizontal();

                            break;
                    }
                }

                if (EditorGUILayout.Foldout(false, "Add Entry", true, EditorStyles.miniButton))
                    jsonList.Entries.Add(new SimpleJsonEntry());
            }

            if (removeEntryIndex != -1) jsonList.Entries.RemoveAt(removeEntryIndex);

            EditorGUILayout.EndVertical();
        }

        #endregion

        #region HELPER METHODS

        /// <summary>
        ///     Draws the json entry delete button.
        /// </summary>
        /// <returns></returns>
        private bool DrawJsonEntryDeleteButton()
        {
            Color previousColor = GUI.backgroundColor;
            GUI.backgroundColor = red;

            bool clicked = GUILayout.Button("X");

            GUI.backgroundColor = previousColor;

            return clicked;
        }


        /// <summary>
        ///     Draw Save Json Button.
        /// </summary>
        /// <returns></returns>
        private bool SaveJson()
        {
            Color previousColor = GUI.backgroundColor;
            GUI.backgroundColor = green;

            bool clicked = GUILayout.Button("Save JSON");

            GUI.backgroundColor = previousColor;

            return clicked;
        }


        private bool CreateJson()
        {
            Color previousColor = GUI.backgroundColor;
            GUI.backgroundColor = orange;

            bool clicked = GUILayout.Button("New Json File");
            GUI.backgroundColor = previousColor;

            return clicked;
        }

        #endregion
    }
}
#endif