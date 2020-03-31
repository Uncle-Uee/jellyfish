// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using SOFlow.Internal;
using UnityEditorInternal;
using UnityEditor;
using UnityEngine;

namespace JellyFish.PlayerInput
{
    [CustomPropertyDrawer(typeof(GameInput))]
    public class GameInputDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty keys = property.FindPropertyRelative("Keys");

            DrawKeyList(position, keys, SOFlowEditorSettings.TertiaryLayerColour);

            position.y += (keys.isExpanded ? keys.arraySize + 4 : 4) * EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(position, property.FindPropertyRelative("Event"));
        }

        private void DrawKeyList(Rect position, SerializedProperty property, Color layerColour)
        {
            Rect  originalRect   = new Rect(position);
            int   keyCount       = property.arraySize;
            Color originalColour = GUI.backgroundColor;

            position.y += EditorGUIUtility.singleLineHeight;

            position.height = EditorGUIUtility.singleLineHeight * 3.75f +
                              (property.isExpanded
                                   ? EditorGUIUtility.singleLineHeight * (keyCount - 1)
                                   : -EditorGUIUtility.singleLineHeight);

            EditorGUI.HelpBox(position, "", MessageType.None);

            position.width  = 25f;
            position.height = EditorGUIUtility.singleLineHeight;

            GUI.backgroundColor = SOFlowEditorSettings.AcceptContextColour;

            if(GUI.Button(position, property.isExpanded ? "↑" : "↓")) property.isExpanded = !property.isExpanded;

            GUI.backgroundColor = originalColour;

            Vector2 textSize = SOFlowStyles.BoldCenterLabel.CalcSize(new GUIContent(property.displayName));

            position.width =  textSize.x;
            position.x     += originalRect.width / 2f - textSize.x / 2f;

            GUI.Label(position, property.displayName, SOFlowStyles.BoldCenterLabel);

            textSize = SOFlowStyles.WordWrappedMiniLabel.CalcSize(new GUIContent($"Size: {keyCount}"));

            position.width = textSize.x;
            position.x     = originalRect.width;

            GUI.Label(position, $"Size: {keyCount}", SOFlowStyles.WordWrappedMiniLabel);

            float indentSpace = textSize.x;
            position.y     += EditorGUIUtility.singleLineHeight;
            position.x     =  originalRect.x + indentSpace;
            position.width =  originalRect.width;

            if(property.isExpanded)
                for(int i = 0; i < keyCount; i++)
                {
                    float propertyWidth = originalRect.width * 0.3f;
                    GUI.backgroundColor = layerColour;

                    textSize = SOFlowStyles.Label.CalcSize(new GUIContent("Key"));

                    position.width = textSize.x;

                    GUI.Label(position, "Key", SOFlowStyles.Label);

                    position.x     += position.width + 4f;
                    position.width =  propertyWidth  - 4f;

                    EditorGUI.PropertyField(position, property.GetArrayElementAtIndex(i).FindPropertyRelative("Key"),
                                            GUIContent.none);

                    textSize = SOFlowStyles.Label.CalcSize(new GUIContent("Input Type"));

                    position.x     = originalRect.width / 2f + indentSpace;
                    position.width = textSize.x;

                    GUI.Label(position, "Input Type", SOFlowStyles.Label);

                    position.x     += position.width + 4f;
                    position.width =  propertyWidth  - 4f;

                    EditorGUI.PropertyField(position,
                                            property.GetArrayElementAtIndex(i).FindPropertyRelative("InputType"),
                                            GUIContent.none);

                    position.x     = originalRect.width + indentSpace / 1.5f;
                    position.width = 25f;

                    GUI.backgroundColor = SOFlowEditorSettings.DeclineContextColour;

                    if(GUI.Button(position, "-"))
                    {
                        property.DeleteArrayElementAtIndex(i--);
                        keyCount--;
                    }

                    GUI.backgroundColor =  originalColour;
                    position.y          += EditorGUIUtility.singleLineHeight;
                    position.x          =  originalRect.x + indentSpace;
                }

            textSize = EditorStyles.boldLabel.CalcSize(new GUIContent($"Add {property.displayName} Entry"));

            position.x     =  originalRect.width / 2f + 4f;
            position.y     += EditorGUIUtility.singleLineHeight / 2f;
            position.width =  textSize.x;

            GUI.backgroundColor = SOFlowEditorSettings.AcceptContextColour;

            if(GUI.Button(position, $"Add {property.displayName} Entry")) property.InsertArrayElementAtIndex(keyCount);

            position.y          += EditorGUIUtility.singleLineHeight / 2f;
            GUI.backgroundColor =  originalColour;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            int persistentArgumentsCount = 0;

            SerializedProperty persistentCalls = property.FindPropertyRelative("Event._PersistentCalls");

            for(int i = 0, condition = persistentCalls.arraySize; i < condition; i++)
            {
                SerializedProperty persistentCall      = persistentCalls.GetArrayElementAtIndex(i);
                SerializedProperty persistentArguments = persistentCall.FindPropertyRelative("_PersistentArguments");
                persistentArgumentsCount += persistentArguments.arraySize;
            }

            ReorderableList eventDrawer = new ReorderableList(property.serializedObject, persistentCalls)
                                          {
                                              elementHeight =
                                                  EditorGUIUtility.singleLineHeight * 1.45f,
                                              headerHeight = 0f
                                          };

            SerializedProperty keys = property.FindPropertyRelative("Keys");

            SerializedProperty events = property.FindPropertyRelative("Event");

            return eventDrawer.GetHeight() +
                   (keys.isExpanded ? keys.arraySize + 4 : 4) * EditorGUIUtility.singleLineHeight -
                   (events.isExpanded
                        ? 0f
                        : EditorGUIUtility.singleLineHeight * Mathf.Max(1, eventDrawer.count) *
                          1.45f
                   ) +
                   (events.isExpanded
                        ? EditorGUIUtility.singleLineHeight * 1.125f * persistentArgumentsCount
                        : 0f);
        }
    }
}
#endif