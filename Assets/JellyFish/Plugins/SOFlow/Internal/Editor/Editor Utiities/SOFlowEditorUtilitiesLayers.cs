// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;

namespace SOFlow.Internal
{
    public static partial class SOFlowEditorUtilities
    {
        /// <summary>
        ///     Draws the primary colour layer.
        ///     <param name="action"></param>
        ///     <param name="options"></param>
        /// </summary>
        public static void DrawPrimaryLayer(Action                   action,
                                            params GUILayoutOption[] options)
        {
            DrawColourLayer(SOFlowEditorSettings.PrimaryLayerColour, action, options);
        }

        /// <summary>
        ///     Draws the secondary colour layer.
        ///     <param name="action"></param>
        ///     <param name="options"></param>
        /// </summary>
        public static void DrawSecondaryLayer(Action                   action,
                                              params GUILayoutOption[] options)
        {
            DrawColourLayer(SOFlowEditorSettings.SecondaryLayerColour, action, options);
        }

        /// <summary>
        ///     Draws the tertiary colour layer.
        ///     <param name="action"></param>
        ///     <param name="options"></param>
        /// </summary>
        public static void DrawTertiaryLayer(Action                   action,
                                             params GUILayoutOption[] options)
        {
            DrawColourLayer(SOFlowEditorSettings.TertiaryLayerColour, action, options);
        }

        /// <summary>
        ///     Draws a custom colour layer.
        /// </summary>
        /// <param name="colour"></param>
        /// <param name="action"></param>
        /// <param name="options"></param>
        public static void DrawColourLayer(Color                    colour, Action action,
                                           params GUILayoutOption[] options)
        {
            Color originalGUIColor = GUI.backgroundColor;
            GUI.backgroundColor = colour;

            EditorGUILayout.BeginVertical(SOFlowStyles.HelpBox, options);
            GUI.backgroundColor = originalGUIColor;

            action?.Invoke();
            EditorGUILayout.EndVertical();
        }

        /// <summary>
        ///     Draws a custom horizontal colour layer.
        /// </summary>
        /// <param name="colour"></param>
        /// <param name="action"></param>
        /// <param name="options"></param>
        public static void DrawHorizontalColourLayer(Color                    colour, Action action,
                                                     params GUILayoutOption[] options)
        {
            Color originalGUIColor = GUI.backgroundColor;
            GUI.backgroundColor = colour;

            EditorGUILayout.BeginHorizontal(SOFlowStyles.HelpBox, options);
            GUI.backgroundColor = originalGUIColor;

            action?.Invoke();
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// Draws a custom header colour layer.
        /// </summary>
        /// <param name="colour"></param>
        /// <param name="action"></param>
        /// <param name="options"></param>
        public static void DrawHeaderColourLayer(Color colour, Action action, params GUILayoutOption[] options)
        {
            Color originalGUIColor = GUI.backgroundColor;
            GUI.backgroundColor = colour;

            EditorGUILayout.BeginHorizontal(SOFlowStyles.HeaderHelpBox, options);
            GUI.backgroundColor = originalGUIColor;

            action?.Invoke();
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        ///     Draws a custom scroll view colour layer.
        /// </summary>
        /// <param name="colour"></param>
        /// <param name="scrollPosition"></param>
        /// <param name="action"></param>
        /// <param name="options"></param>
        public static void DrawScrollViewColourLayer(Color                    colour, ref Vector2 scrollPosition,
                                                     Action                   action,
                                                     params GUILayoutOption[] options)
        {
            Color originalGUIColor = GUI.backgroundColor;
            GUI.backgroundColor = colour;

            scrollPosition      = EditorGUILayout.BeginScrollView(scrollPosition, SOFlowStyles.HelpBox, options);
            GUI.backgroundColor = originalGUIColor;

            action?.Invoke();
            EditorGUILayout.EndScrollView();
        }
    }
}
#endif