// // Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/
//
// #if UNITY_EDITOR
// using System.Collections.Generic;
// using System;
// using System.Reflection;
// using Harmony;
// using SOFlow.Data.Events;
// using UnityEditor;
// using UnityEngine;
//
// namespace SOFlow.Internal
// {
//     [CustomEditor(typeof(GameObject), true), CanEditMultipleObjects]
//     public class GameObjectInspectorEditor : Editor
//     {
//         /// <summary>
//         /// The Game Object Inspector type.
//         /// </summary>
//         private static Type _gameObjectInspector;
//
//         /// <summary>
//         /// The Game Object Inspector editor reference.
//         /// </summary>
//         private static Editor _gameObjectInspectorEditor;
//
//         /// <summary>
//         /// The Game Object Inspector OnHeaderGUI method reference.
//         /// </summary>
//         private readonly MethodInfo _gameObjectInspectorOnHeaderGUI;
//
//         /// <summary>
//         /// The event button content.
//         /// </summary>
//         private GUIContent _eventButtonContent = new GUIContent();
//
//         /// <summary>
//         /// The logo content.
//         /// </summary>
//         private GUIContent _logoContent = new GUIContent();
//
//         /// <summary>
//         /// Indicates whether the original Game Object inspector has been patched.
//         /// </summary>
//         private bool _originalInspectorPatched = false;
//
//         public GameObjectInspectorEditor()
//         {
//             _gameObjectInspector = Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.GameObjectInspector");
//
//             _gameObjectInspectorOnHeaderGUI =
//                 _gameObjectInspector.GetMethod("OnHeaderGUI",
//                                                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
//         }
//
//         private void Awake()
//         {
//             _eventButtonContent.image   = Resources.Load<Texture2D>("Game Event Icon");
//             _eventButtonContent.tooltip = "Add Simple Game Event Listener Component";
//             _logoContent.image          = Resources.Load<Texture2D>("SOFlow Logo");
//         }
//
//         protected override void OnHeaderGUI()
//         {
//             _gameObjectInspectorEditor = CreateEditor(target, _gameObjectInspector);
//             _gameObjectInspectorOnHeaderGUI.Invoke(_gameObjectInspectorEditor, null);
//
//             if(!_originalInspectorPatched)
//             {
//                 _originalInspectorPatched = true;
//
//                 HarmonyInstance harmonyInstance = HarmonyInstance.Create(Application.identifier);
//
//                 harmonyInstance.Patch(AccessTools.Method(_gameObjectInspectorEditor.GetType(), "ClearPreviewCache"),
//                                       new HarmonyMethod(typeof(GameObjectInspectorEditor).GetMethod(nameof(
//                                                                                                         PreClearPreviewCache
//                                                                                                     ),
//                                                                                                     BindingFlags
//                                                                                                        .Static |
//                                                                                                     BindingFlags
//                                                                                                        .Public)));
//             }
//
//             SOFlowEditorUtilities.DrawHeaderColourLayer(SOFlowEditorSettings.PrimaryLayerColour, DrawHeaderItems);
//         }
//
//         /// <summary>
//         /// Draws custom header items to the top of all Game Object inspectors.
//         /// </summary>
//         private void DrawHeaderItems()
//         {
//             EditorGUILayout.LabelField(_logoContent);
//
//             GUILayout.FlexibleSpace();
//
//             if(SOFlowEditorUtilities.DrawColourButton(_eventButtonContent, SOFlowEditorSettings.AcceptContextColour,
//                                                       SOFlowStyles.IconButton,
//                                                       SOFlowEditorUtilities.StandardButtonSettings))
//             {
//                 if(Selection.activeGameObject != null)
//                 {
//                     Undo.AddComponent<SimpleGameEventListener>(Selection.activeGameObject);
//                 }
//             }
//         }
//
//         public override void OnInspectorGUI()
//         {
//         }
//
//         /// <summary>
//         /// PATCH - Ensures the internal preview cache for the original Game Object inspector has been initialized before being accessed.
//         /// </summary>
//         /// <param name="__instance"></param>
//         public static void PreClearPreviewCache(object __instance)
//         {
//             FieldInfo previewCache =
//                 AccessTools.Field(_gameObjectInspectorEditor.GetType(), "m_PreviewCache");
//
//             Dictionary<int, Texture> previewCacheValue = (Dictionary<int, Texture>)previewCache.GetValue(__instance);
//
//             if(previewCacheValue == null || previewCacheValue.Equals(null))
//             {
//                 previewCacheValue = new Dictionary<int, Texture>();
//                 previewCache.SetValue(_gameObjectInspectorEditor, previewCacheValue);
//             }
//         }
//     }
// }
// #endif