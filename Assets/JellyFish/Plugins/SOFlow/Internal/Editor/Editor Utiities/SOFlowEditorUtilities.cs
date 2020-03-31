// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using Object = UnityEngine.Object;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SOFlow.Internal
{
    public static partial class SOFlowEditorUtilities
    {
        /// <summary>
        /// The standard GUI button settings.
        /// </summary>
        public static GUILayoutOption[] StandardButtonSettings = {
                                                                     GUILayout.MaxWidth(20f), GUILayout.MaxHeight(16f)
                                                                 };
        
        /// <summary>
        ///     The original GUI colour.
        /// </summary>
        private static Color _originalGUIColour;
        
        /// <summary>
        /// The cached button GUIContent.
        /// </summary>
        private static GUIContent _buttonContent = new GUIContent();
        
        /// <summary>
        /// The cached text area button GUIContent.
        /// </summary>
        private static GUIContent _textAreaButtonContent = new GUIContent();
        
        /// <summary>
        /// The cached object history button GUIContent.
        /// </summary>
        private static GUIContent _objectHistoryButtonContent = new GUIContent();
        
        /// <summary>
        /// The cached numeric slider button GUIContent.
        /// </summary>
        private static GUIContent _numericSliderButtonContent = new GUIContent();

        /// <summary>
        /// Loads all cached GUIContent data.
        /// </summary>
        [UnityEditor.Callbacks.DidReloadScripts]
        [InitializeOnLoadMethod]
        public static void LoadCachedContent()
        {
            _textAreaButtonContent.image = Resources.Load<Texture2D>("Text Area Icon");
            _objectHistoryButtonContent.image = Resources.Load<Texture2D>("Object History Icon");
            _numericSliderButtonContent.image = Resources.Load<Texture2D>("Numeric Slider Icon");
        }

        /// <summary>
        ///     Saves the given colour to the given key in the EditorPrefs.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="colour"></param>
        public static void SaveColour(string key, Color colour)
        {
            EditorPrefs.SetFloat(key + "-r", colour.r);
            EditorPrefs.SetFloat(key + "-g", colour.g);
            EditorPrefs.SetFloat(key + "-b", colour.b);
            EditorPrefs.SetFloat(key + "-a", colour.a);
        }

        /// <summary>
        ///     Loads the saved colour with the given key from the EditorPrefs.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Color LoadColour(string key)
        {
            Color result = Color.clear;

            result.r = EditorPrefs.GetFloat(key + "-r");
            result.g = EditorPrefs.GetFloat(key + "-g");
            result.b = EditorPrefs.GetFloat(key + "-b");
            result.a = EditorPrefs.GetFloat(key + "-a");

            return result;
        }

        /// <summary>
        /// Saves the SOFlow numeric slider data.
        /// </summary>
        public static void SaveNumericSliderData()
        {
            NumericSliderList sliderData = new NumericSliderList();

            foreach(KeyValuePair<int, NumericSliderData> data in _numericSliders)
            {
                sliderData.SliderData.Add(data.Value);
            }

            try
            {
                File.WriteAllText(Path.Combine(Application.persistentDataPath, _numericSlidersFile), EditorJsonUtility.ToJson(sliderData));
            }
            catch(Exception e)
            {
                Debug.LogError($"Failed to save numeric slider data.\n\n{e.Message}");
            }
        }

        /// <summary>
        /// Loads the SOFlow numeric slider data.
        /// </summary>
        [UnityEditor.Callbacks.DidReloadScripts]
        [InitializeOnLoadMethod]
        public static void LoadNumericSliderData()
        {
            try
            {
                string filePath = Path.Combine(Application.persistentDataPath, _numericSlidersFile);

                if(File.Exists(filePath))
                {
                    _numericSliders.Clear();
                    
                    NumericSliderList sliderData = new NumericSliderList();
                    EditorJsonUtility.FromJsonOverwrite(File.ReadAllText(filePath), sliderData);

                    foreach(NumericSliderData data in sliderData.SliderData)
                    {
                        _numericSliders.Add(data.SliderID, data);
                    }
                }
            }
            catch(Exception e)
            {
                Debug.LogError($"Failed to load numeric slider data.\n\n{e.Message}");
            }
        }

        /// <summary>
        /// Saves the SOFlow text area data.
        /// </summary>
        public static void SaveTextAreaData()
        {
            TextAreaList textAreaData = new TextAreaList();

            foreach(KeyValuePair<int, TextAreaData> data in _textAreas)
            {
                textAreaData.TextAreaData.Add(data.Value);
            }

            try
            {
                File.WriteAllText(Path.Combine(Application.persistentDataPath, _textAreasFile),
                                  EditorJsonUtility.ToJson(textAreaData));
            }
            catch(Exception e)
            {
                Debug.LogError($"Failed to save text area data.\n\n{e.Message}");
            }
        }

        /// <summary>
        /// Loads the SOFlow text area data.
        /// </summary>
        [UnityEditor.Callbacks.DidReloadScripts]
        [InitializeOnLoadMethod]
        public static void LoadTextAreaData()
        {
            try
            {
                string filePath = Path.Combine(Application.persistentDataPath, _textAreasFile);

                if(File.Exists(filePath))
                {
                    _textAreas.Clear();

                    TextAreaList textAreaData = new TextAreaList();
                    EditorJsonUtility.FromJsonOverwrite(File.ReadAllText(filePath), textAreaData);

                    foreach(TextAreaData data in textAreaData.TextAreaData)
                    {
                        _textAreas.Add(data.TextAreaID, data);
                    }
                }
            }
            catch(Exception e)
            {
                Debug.LogError($"Failed to load text area data.\n\n{e.Message}");
            }
        }

        /// <summary>
        ///     Adjusts the text colouring to display clearly for any contrast.
        /// </summary>
        /// <param name="colour"></param>
        public static void AdjustTextContrast(Color colour)
        {
            _originalGUIColour = GUI.contentColor;

            float contrast = 0.2126f * colour.r +
                             0.7152f * colour.g +
                             0.0722f * colour.b;

            GUI.contentColor = contrast < 0.5f ? Color.white : Color.black;
        }

        /// <summary>
        ///     Reverts any contrast adjustments that have been made on text colouring.
        /// </summary>
        public static void RestoreTextContrast()
        {
            GUI.contentColor = _originalGUIColour;
        }

        /// <summary>
        ///     Draws a custom coloured button.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="colour"></param>
        /// <param name="style"></param>
        /// <param name="layoutOptions"></param>
        /// <returns></returns>
        public static bool DrawColourButton(string                   text, Color? colour = null, GUIStyle style = null,
                                            params GUILayoutOption[] layoutOptions)
        {
            _buttonContent.text = text;
            return DrawColourButton(_buttonContent, colour, style, layoutOptions);
        }

        /// <summary>
        ///     Draws a custom coloured button.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="colour"></param>
        /// <param name="style"></param>
        /// <param name="layoutOptions"></param>
        /// <returns></returns>
        public static bool DrawColourButton(GUIContent content, Color? colour = null, GUIStyle style = null,
                                            params GUILayoutOption[] layoutOptions)
        {
            Color originalGUIColor = GUI.backgroundColor;
            GUI.backgroundColor = colour ?? Color.white;

            bool result = style == null
                              ? GUILayout.Button(content, SOFlowStyles.Button, layoutOptions)
                              : GUILayout.Button(content, style,               layoutOptions);

            GUI.backgroundColor = originalGUIColor;

            return result;
        }

        /// <summary>
        /// Attempts to get the local object ID within the file the object is contained in.
        /// Returns -1 if no ID is found.
        /// </summary>
        /// <param name="targetObject"></param>
        /// <returns></returns>
        public static long GetObjectLocalIDInFile(Object targetObject)
        {
            long id = -1;
            SerializedObject serializedObject = new SerializedObject(targetObject);

            PropertyInfo inspectorModeInfo = typeof(SerializedObject).GetProperty("inspectorMode", BindingFlags.NonPublic | BindingFlags.Instance);

            if(inspectorModeInfo != null)
            {
                inspectorModeInfo.SetValue(serializedObject, InspectorMode.Debug, null);
            }

            SerializedProperty localIDProperty = serializedObject.FindProperty("m_LocalIdentfierInFile");

            if(localIDProperty != null)
            {
                id = localIDProperty.longValue;
            }

            return id;
        }
    }
}
#endif