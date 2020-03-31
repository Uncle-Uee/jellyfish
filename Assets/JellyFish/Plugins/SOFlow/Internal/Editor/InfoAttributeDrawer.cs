// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace SOFlow.Internal
{
    [CustomPropertyDrawer(typeof(InfoAttribute))]
    public class InfoDrawer : PropertyDrawer
    {
        private Vector2    _infoBoxSize = Vector2.zero;
        private GUIContent _infoButtonContent;

        private string _infoLabel = "";

        // Drawer cached values.
        private float _infoSpace;
        private float _initialXPosition;
        private float _previousWidth;
        private float _totalWidth;

        /// <inheritdoc />
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if(SOFlowEditorSettings.DrawDefaultProperties)
            {
                EditorGUI.PropertyField(position, property, label);
            }
            else
            {
                if(position.width <= 1) return;

                label = EditorGUI.BeginProperty(position, label, property);

                InfoAttribute infoAttribute = attribute as InfoAttribute;

                _initialXPosition = position.x;
                _totalWidth       = position.width;

                if(_totalWidth != _previousWidth)
                {
                    _previousWidth = _totalWidth;

                    if(property.serializedObject != null && property.serializedObject.targetObject)
                        EditorUtility.SetDirty(property.serializedObject.targetObject);
                }

                _infoSpace = SOFlowStyles.HelpBox.CalcSize(new GUIContent("   ")).x;

                _infoButtonContent = new GUIContent
                                     {
                                         text = "?", tooltip = infoAttribute.Info
                                     };

                _infoLabel   = $"[{label.text} INFO]\n{infoAttribute.Info}";
                _infoBoxSize = SOFlowStyles.HelpBox.CalcSize(new GUIContent(_infoLabel));

                position.width  -= _infoSpace;
                position.height =  EditorGUIUtility.singleLineHeight;

                EditorGUI.PropertyField(position, property, label);

                position.x     += position.width + _infoSpace * 0.125f;
                position.width =  _infoSpace;

                if(GUI.Button(position, _infoButtonContent)) infoAttribute.InfoViewed = !infoAttribute.InfoViewed;

                if(infoAttribute.InfoViewed)
                {
                    position.x     =  _initialXPosition;
                    position.y     += EditorGUIUtility.singleLineHeight;
                    position.width =  _totalWidth;

                    position.height = _infoBoxSize.y +
                                      EditorGUIUtility.singleLineHeight * Mathf.Round(_infoBoxSize.x / _totalWidth);

                    Color previousColor = GUI.backgroundColor;
                    GUI.backgroundColor = SOFlowEditorSettings.SecondaryLayerColour;
                    EditorGUI.LabelField(position, _infoLabel, SOFlowStyles.HelpBox);
                    GUI.backgroundColor = previousColor;
                }

                EditorGUI.EndProperty();
            }
        }

        /// <inheritdoc />
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if(!SOFlowEditorSettings.DrawDefaultProperties)
            {
                InfoAttribute infoAttribute = attribute as InfoAttribute;

                if(infoAttribute.InfoViewed)
                    return _infoBoxSize.y +
                           EditorGUIUtility.singleLineHeight * (Mathf.Round(_infoBoxSize.x / _totalWidth) + 1f);
            }

            return base.GetPropertyHeight(property, label);
        }
    }
}
#endif