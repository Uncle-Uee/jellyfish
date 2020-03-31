// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using System;
using SOFlow.Extensions;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using System.Collections.Generic;
using UnityEditorInternal;

namespace SOFlow.Internal
{
    public static partial class SOFlowEditorUtilities
    {
        /// <summary>
        ///     The list of cached reorderable lists.
        /// </summary>
        private static readonly Dictionary<string, ReorderableList> _cachedReorderableLists =
            new Dictionary<string, ReorderableList>();

        /// <summary>
        /// The GUI width of numeric slider ranges.
        /// </summary>
        private static readonly float _numericSliderRangeWidth = 50f;

        /// <summary>
        /// The previous mouse Y position.
        /// </summary>
        private static float _previousMouseY = 0f;

        /// <summary>
        ///     Draws the given property for the serialized object if the property is available.
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <param name="property"></param>
        /// <param name="includeChildren"></param>
        public static void DrawProperty(this SerializedObject serializedObject, string property,
                                        bool                  includeChildren = true)
        {
            SerializedProperty serializedProperty = serializedObject.FindProperty(property);

            if(serializedProperty != null) DrawPropertyWithNullCheck(serializedProperty, includeChildren);
        }

        /// <summary>
        ///     Draws a list for the component property.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="layerColour"></param>
        /// <param name="serializedObject"></param>
        public static void DrawListComponentProperty(SerializedObject serializedObject, SerializedProperty property,
                                                     Color            layerColour)
        {
            EditorGUILayout.BeginHorizontal();

            if(DrawColourButton(property.isExpanded ? "↑" : "↓", SOFlowEditorSettings.AcceptContextColour, null,
                                GUILayout.MaxWidth(25f)))
                property.isExpanded = !property.isExpanded;

            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField(property.displayName, SOFlowStyles.BoldCenterLabel);
            GUILayout.FlexibleSpace();

            EditorGUILayout.LabelField($"Size: {property.arraySize}", SOFlowStyles.WordWrappedMiniLabel);

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            GUIContent addButtonContent = new GUIContent($"Add {property.displayName} Entry");

            if(DrawColourButton(addButtonContent.text, SOFlowEditorSettings.AcceptContextColour, null,
                                GUILayout.MaxWidth(EditorStyles.label.CalcSize(addButtonContent).x + 24)))
                property.InsertArrayElementAtIndex(property.arraySize);

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            if(property.isExpanded)
                DrawColourLayer(layerColour, () =>
                                             {
                                                 ReorderableList reorderableList =
                                                     GetCachedReorderableList(serializedObject, property);

                                                 if(reorderableList == null)
                                                 {
                                                     reorderableList =
                                                         new ReorderableList(serializedObject, property, true, false,
                                                                             true, true)
                                                         {
                                                             showDefaultBackground = false, headerHeight = 0f
                                                         };

                                                     reorderableList.onCanRemoveCallback += list => list.count > 0;

                                                     reorderableList.drawFooterCallback += rect =>
                                                                                           {
                                                                                               Color originalGUIColor =
                                                                                                   GUI.backgroundColor;

                                                                                               GUI.backgroundColor =
                                                                                                   SOFlowEditorSettings
                                                                                                      .AcceptContextColour;

                                                                                               ReorderableList
                                                                                                  .defaultBehaviours
                                                                                                  .DrawFooter(rect,
                                                                                                              reorderableList);

                                                                                               GUI.backgroundColor =
                                                                                                   originalGUIColor;
                                                                                           };

                                                     reorderableList.drawElementCallback +=
                                                         (rect, index, active, focused) =>
                                                         {
                                                             if(reorderableList.serializedProperty.arraySize <= index)
                                                                 return;

                                                             rect.x     -= 20f;
                                                             rect.width += 24f;

                                                             EditorGUI.HelpBox(rect, "", MessageType.None);

                                                             rect.x     += 20f;
                                                             rect.width -= 24f;

                                                             SerializedProperty indexProperty =
                                                                 reorderableList
                                                                    .serializedProperty.GetArrayElementAtIndex(index);

                                                             string             entryTitle     = null;
                                                             SerializedProperty copiedProperty = indexProperty.Copy();

                                                             if(copiedProperty.Next(true))
                                                                 entryTitle =
                                                                     copiedProperty.propertyType ==
                                                                     SerializedPropertyType.String
                                                                         ? copiedProperty.stringValue
                                                                         : null;

                                                             rect.height =
                                                                 EditorGUI.GetPropertyHeight(indexProperty,
                                                                                             GUIContent.none, true);

                                                             rect.y++;

                                                             if(indexProperty.hasVisibleChildren)
                                                             {
                                                                 rect.x     += 12f;
                                                                 rect.width -= 12f;
                                                             }

                                                             rect.width -= 25f;

                                                             EditorGUI.PropertyField(rect, indexProperty,
                                                                                     new
                                                                                         GUIContent($"Entry {index + 1}{(entryTitle == null ? "" : $" | {entryTitle}")}"),
                                                                                     true);

                                                             rect.x      += rect.width;
                                                             rect.width  =  25f;
                                                             rect.height =  EditorGUIUtility.singleLineHeight;

                                                             Color originalGUIColor = GUI.backgroundColor;

                                                             GUI.backgroundColor =
                                                                 SOFlowEditorSettings.DeclineContextColour;

                                                             if(EditorGUI.Toggle(rect, false, SOFlowStyles.Button))
                                                                 reorderableList
                                                                    .serializedProperty
                                                                    .DeleteArrayElementAtIndex(index);

                                                             EditorGUI.LabelField(rect, "-",
                                                                                  SOFlowStyles.CenteredLabel);

                                                             GUI.backgroundColor = originalGUIColor;

                                                             reorderableList.elementHeight = rect.height + 4f;
                                                         };

                                                     reorderableList.elementHeightCallback +=
                                                         index =>
                                                         {
                                                             if(reorderableList.serializedProperty.arraySize <= index)
                                                                 return EditorGUIUtility.singleLineHeight;

                                                             return Mathf.Max(EditorGUIUtility.singleLineHeight,
                                                                              EditorGUI
                                                                                 .GetPropertyHeight(reorderableList.serializedProperty.GetArrayElementAtIndex(index),
                                                                                                    GUIContent.none,
                                                                                                    true)) + 4f;
                                                         };

                                                     _cachedReorderableLists
                                                        .Add($"{serializedObject.targetObject.name}{property.propertyPath}",
                                                             reorderableList);
                                                 }
                                                 else
                                                 {
                                                     reorderableList.serializedProperty = property;
                                                 }

                                                 try
                                                 {
                                                     reorderableList.DoLayoutList();
                                                 }
                                                 catch(Exception)
                                                 {
                                                     // Ignore
                                                 }

                                                 Rect listRect =
                                                     GUILayoutUtility.GetLastRect();

                                                 float listHeight = reorderableList.GetHeight();

                                                 listRect.y      -= listHeight - reorderableList.footerHeight;
                                                 listRect.height += listHeight - reorderableList.footerHeight;

                                                 if(listRect.Contains(Event.current.mousePosition))
                                                 {
                                                     if(Event.current.type == EventType.DragUpdated)
                                                     {
                                                         DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                                                         Event.current.Use();
                                                     }
                                                     else if(Event.current.type == EventType.DragPerform)
                                                     {
                                                         if(DragAndDrop.objectReferences.Length > 0)
                                                         {
                                                             foreach(Object _object in DragAndDrop.objectReferences)
                                                             {
                                                                 SerializedProperty newProperty = null;

                                                                 try
                                                                 {
                                                                     property.InsertArrayElementAtIndex(property
                                                                                                           .arraySize);

                                                                     newProperty =
                                                                         property.GetArrayElementAtIndex(property
                                                                                                            .arraySize -
                                                                                                         1);

                                                                     newProperty.objectReferenceValue = _object;

                                                                     if(newProperty.objectReferenceValue == null)
                                                                         throw new Exception();
                                                                 }
                                                                 catch(Exception e)
                                                                 {
                                                                     Debug
                                                                        .LogWarning($"Object [{_object?.GetType()}] not compatible with this list.\n" +
                                                                                    $"Expected object type : [{property.arrayElementType}]\n\n"       +
                                                                                    $"{e.Message}\n\n{e.StackTrace}");

                                                                     Debug
                                                                        .Log("Attempting to find embedded compatible types.");

                                                                     if(SetInnerPropertyReference(newProperty, _object))
                                                                     {
                                                                         Debug.Log("Compatible embedded type found.");
                                                                     }
                                                                     else
                                                                     {
                                                                         Debug
                                                                            .LogWarning("Unable to find compatible embedded type.");

                                                                         property.DeleteArrayElementAtIndex(property
                                                                                                               .arraySize -
                                                                                                            1);
                                                                     }
                                                                 }
                                                             }

                                                             serializedObject.ApplyModifiedProperties();
                                                         }

                                                         Event.current.Use();
                                                     }
                                                 }
                                             });
        }

        /// <summary>
        ///     Attempts to set an inner property reference for the given property to the provided object value.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="_object"></param>
        /// <returns></returns>
        private static bool SetInnerPropertyReference(SerializedProperty property, Object _object)
        {
            property.NextVisible(true);

            do
            {
                try
                {
                    Type instanceType = TypeExtensions.GetInstanceType(property.type);

                    if(instanceType == _object.GetType())
                    {
                        property.objectReferenceValue = _object;

                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            } while(property.NextVisible(false));

            return false;
        }

        /// <summary>
        ///     Gets the cached version of the ReorderableList linked with the given property.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="serializedObject"></param>
        /// <returns></returns>
        private static ReorderableList GetCachedReorderableList(SerializedObject   serializedObject,
                                                                SerializedProperty property)
        {
            ReorderableList result = null;

            _cachedReorderableLists.TryGetValue($"{serializedObject.targetObject.name}{property.propertyPath}",
                                                out result);

            return result;
        }

        /// <summary>
        ///     Draws all properties for the provided serialized object within custom layers.
        /// </summary>
        /// <param name="serializedObject"></param>
        public static void DrawLayeredProperties(SerializedObject serializedObject)
        {
            DrawPrimaryLayer(() =>
                             {
                                 SerializedProperty nextProperty = serializedObject.GetIterator();

                                 if(nextProperty.NextVisible(true))
                                     do
                                     {
                                         if(nextProperty.isArray &&
                                            nextProperty.propertyType != SerializedPropertyType.String)
                                         {
                                             DrawSecondaryLayer(() =>
                                                                {
                                                                    DrawListComponentProperty(serializedObject,
                                                                                              nextProperty,
                                                                                              SOFlowEditorSettings
                                                                                                 .TertiaryLayerColour);
                                                                });
                                         }
                                         else
                                         {
                                             SerializedProperty objectProperty =
                                                 serializedObject.FindProperty(nextProperty.name);

                                             DrawPropertyWithNullCheck(objectProperty);
                                         }
                                     } while(nextProperty.NextVisible(false));
                             });
        }

        /// <summary>
        ///     Draws the given property and indicates whether the property object value is null.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="includeChildren"></param>
        /// <param name="propertyLabel"></param>
        /// <param name="layoutOptions"></param>
        public static void DrawPropertyWithNullCheck(SerializedProperty       property, bool includeChildren = true,
                                                     string                   propertyLabel = null,
                                                     params GUILayoutOption[] layoutOptions)
        {
            bool  nullDetected  = false;
            Color currentColour = _originalGUIColour;

            if(property.propertyType == SerializedPropertyType.ObjectReference)
            {
                nullDetected = property.objectReferenceValue == null;

                if(nullDetected)
                {
                    currentColour       = GUI.backgroundColor;
                    GUI.backgroundColor = SOFlowEditorSettings.DeclineContextColour;
                }
            }

            if(property.propertyType == SerializedPropertyType.Integer ||
               property.propertyType == SerializedPropertyType.Float)
            {
                int propertyID =
                    $"{GetObjectLocalIDInFile(property.serializedObject.targetObject)}{property.propertyPath}"
                       .GetHashCode();

                NumericSliderData sliderData;

                if(!_numericSliders.TryGetValue(propertyID, out sliderData))
                {
                    sliderData = new NumericSliderData
                                 {
                                     SliderID = propertyID
                                 };

                    _numericSliders.Add(propertyID, sliderData);
                }

                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(propertyLabel ?? property.displayName,
                                           GUILayout.Width(EditorGUIUtility.labelWidth - 4f));

                EditorGUILayout.BeginHorizontal();

                bool sliderDatachanged = false;

                if(sliderData.SliderActive)
                {
                    if(property.propertyType == SerializedPropertyType.Integer)
                    {
                        EditorGUI.BeginChangeCheck();

                        sliderData.SliderMinValue =
                            EditorGUILayout.IntField((int)sliderData.SliderMinValue, SOFlowStyles.CenterTextHelpBox,
                                                     GUILayout.MaxWidth(_numericSliderRangeWidth));

                        sliderDatachanged = EditorGUI.EndChangeCheck();

                        property.intValue = EditorGUILayout.IntSlider(property.intValue, (int)sliderData.SliderMinValue,
                                                                      (int)sliderData.SliderMaxValue);

                        EditorGUI.BeginChangeCheck();

                        sliderData.SliderMaxValue =
                            EditorGUILayout.IntField((int)sliderData.SliderMaxValue, SOFlowStyles.CenterTextHelpBox,
                                                     GUILayout.MaxWidth(_numericSliderRangeWidth));

                        sliderDatachanged = sliderDatachanged || EditorGUI.EndChangeCheck();
                    }
                    else
                    {
                        EditorGUI.BeginChangeCheck();

                        sliderData.SliderMinValue =
                            EditorGUILayout.FloatField(sliderData.SliderMinValue, SOFlowStyles.CenterTextHelpBox,
                                                       GUILayout.MaxWidth(_numericSliderRangeWidth));

                        sliderDatachanged = EditorGUI.EndChangeCheck();

                        property.floatValue = EditorGUILayout.Slider(property.floatValue, sliderData.SliderMinValue,
                                                                     sliderData.SliderMaxValue);

                        EditorGUI.BeginChangeCheck();

                        sliderData.SliderMaxValue =
                            EditorGUILayout.FloatField(sliderData.SliderMaxValue, SOFlowStyles.CenterTextHelpBox,
                                                       GUILayout.MaxWidth(_numericSliderRangeWidth));

                        sliderDatachanged = sliderDatachanged || EditorGUI.EndChangeCheck();
                    }
                }
                else
                {
                    if(property.propertyType == SerializedPropertyType.Integer)
                    {
                        property.intValue = EditorGUILayout.IntField(property.intValue, layoutOptions);
                    }
                    else
                    {
                        property.floatValue = EditorGUILayout.FloatField(property.floatValue, layoutOptions);
                    }
                }

                if(DrawColourButton(_numericSliderButtonContent,
                                    sliderData.SliderActive ? SOFlowEditorSettings.TertiaryLayerColour : SOFlowEditorSettings.AcceptContextColour,
                                    sliderData.SliderActive ? SOFlowStyles.PressedIconButton : SOFlowStyles.IconButton,
                                    StandardButtonSettings))
                {
                    sliderData.SliderActive = !sliderData.SliderActive;
                    sliderDatachanged       = true;
                }

                if(sliderDatachanged)
                {
                    SaveNumericSliderData();
                }

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndHorizontal();
            }
            else if(property.propertyType == SerializedPropertyType.ObjectReference &&
                    property.propertyPath != "m_Script")
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(propertyLabel ?? property.displayName,
                                           GUILayout.Width(EditorGUIUtility.labelWidth - 4f));

                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.PropertyField(property, GUIContent.none, layoutOptions);

                bool propertyAvailable = ObjectPickerHistoryWindow.PropertyAvailable &&
                                         ObjectPickerHistoryWindow.EditingProperty.propertyPath ==
                                         property.propertyPath;
                
                if(DrawColourButton(_objectHistoryButtonContent,
                                    propertyAvailable ? SOFlowEditorSettings.TertiaryLayerColour : SOFlowEditorSettings.AcceptContextColour,
                                    propertyAvailable ? SOFlowStyles.PressedIconButton : SOFlowStyles.IconButton, StandardButtonSettings))
                {
                    ObjectPickerHistoryWindow.EditingProperty = property;
                    ObjectPickerHistoryWindow.ShowWindow();
                }

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndHorizontal();
            }
            else if(property.propertyType == SerializedPropertyType.String)
            {
                int propertyID =
                    $"{GetObjectLocalIDInFile(property.serializedObject.targetObject)}{property.propertyPath}"
                       .GetHashCode();

                TextAreaData textAreaData;

                if(!_textAreas.TryGetValue(propertyID, out textAreaData))
                {
                    textAreaData = new TextAreaData
                                   {
                                       TextAreaID = propertyID
                                   };

                    _textAreas.Add(propertyID, textAreaData);
                }

                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(propertyLabel ?? property.displayName,
                                           GUILayout.Width(EditorGUIUtility.labelWidth - 4f));

                EditorGUILayout.BeginHorizontal();

                bool  textAreaDataChanged = false;
                Event currentEvent        = Event.current;

                if(textAreaData.TextAreaActive)
                {
                    EditorGUILayout.BeginVertical();

                    property.stringValue = EditorGUILayout.TextArea(property.stringValue, SOFlowStyles.TextArea,
                                                                    GUILayout.Height(EditorGUIUtility.singleLineHeight *
                                                                                     textAreaData.TextAreaHeight));

                    EditorGUILayout.LabelField("", (GUIStyle)"WindowBottomResize");

                    bool dragHandleContainsMouse = GUILayoutUtility.GetLastRect().Contains(currentEvent.mousePosition);

                    if(!TextAreaData.TextAreaDragged            && dragHandleContainsMouse &&
                       currentEvent.type == EventType.MouseDown && currentEvent.button == 0)
                    {
                        TextAreaData.TextAreaDragged  = true;
                        textAreaData.CurrentlyDragged = true;
                        _previousMouseY               = Mathf.Abs(currentEvent.mousePosition.y);
                    }
                    else if(TextAreaData.TextAreaDragged && textAreaData.CurrentlyDragged &&
                            currentEvent.type == EventType.MouseDrag)
                    {
                        textAreaData.TextAreaHeight +=
                            (Mathf.Abs(currentEvent.mousePosition.y) - _previousMouseY) / 16f;

                        textAreaData.TextAreaHeight = Mathf.Max(1f, textAreaData.TextAreaHeight);
                        _previousMouseY             = Mathf.Abs(currentEvent.mousePosition.y);
                        textAreaDataChanged         = true;
                    }
                    else if(TextAreaData.TextAreaDragged && currentEvent.type == EventType.MouseUp)
                    {
                        TextAreaData.TextAreaDragged = false;

                        foreach(KeyValuePair<int, TextAreaData> data in _textAreas)
                        {
                            data.Value.CurrentlyDragged = false;
                        }
                    }

                    EditorGUILayout.EndVertical();
                }
                else
                {
                    property.stringValue = EditorGUILayout.TextField(property.stringValue, layoutOptions);
                }

                if(DrawColourButton(_textAreaButtonContent,
                                    textAreaData.TextAreaActive ? SOFlowEditorSettings.TertiaryLayerColour : SOFlowEditorSettings.AcceptContextColour,
                                    textAreaData.TextAreaActive ? SOFlowStyles.PressedIconButton : SOFlowStyles.IconButton,
                                    StandardButtonSettings))
                {
                    textAreaData.TextAreaActive = !textAreaData.TextAreaActive;
                    textAreaDataChanged         = true;
                }

                if(textAreaDataChanged)
                {
                    SaveTextAreaData();
                }

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                if(propertyLabel == null)
                {
                    using(new EditorGUI.DisabledScope("m_Script" == property.propertyPath))
                    {
                        EditorGUILayout.PropertyField(property, includeChildren, layoutOptions);
                    }
                }
                else
                {
                    using(new EditorGUI.DisabledScope("m_Script" == property.propertyPath))
                    {
                        EditorGUILayout.PropertyField(property, new GUIContent(propertyLabel), includeChildren,
                                                      layoutOptions);
                    }
                }
            }

            if(nullDetected) GUI.backgroundColor = currentColour;
        }
    }
}
#endif