// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using SOFlow.Internal;
using UnityEditor;
using UnityEngine;

namespace JellyFish.Fading
{
    [CustomPropertyDrawer(typeof(Fadable), true)]
    public class FadableDrawer : PropertyDrawer
    {
        /// <summary>
        ///     Indicates whether the fadable object reference has been assigned in the inspector.
        /// </summary>
        private bool _referenceApplied;

        /// <inheritdoc />
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if(SOFlowEditorSettings.DrawDefaultProperties)
            {
                EditorGUI.PropertyField(position, property, label);
            }
            else
            {
                label = EditorGUI.BeginProperty(position, label, property);

                position.height = EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(position, property, label);

                if(property.objectReferenceValue == null)
                {
                    _referenceApplied = false;
                }
                else
                {
                    _referenceApplied = true;

                    Fadable fadable = property.objectReferenceValue as Fadable;

                    if(fadable != null)
                    {
                        position.y += EditorGUIUtility.singleLineHeight;

                        position.width /= 3f;

                        fadable.AlphaOnly = EditorGUI.ToggleLeft(position, "Alpha Only", fadable.AlphaOnly,
                                                                 SOFlowStyles.CenteredLabel);

                        position.x += position.width;

                        fadable.InvertAlpha = EditorGUI.ToggleLeft(position, "Invert Alpha", fadable.InvertAlpha,
                                                                   SOFlowStyles.CenteredLabel);

                        position.x += position.width;

                        fadable.InvertPercentage = EditorGUI.ToggleLeft(position, "Invert %", fadable.InvertPercentage,
                                                                        SOFlowStyles.CenteredLabel);
                    }
                }

                EditorGUI.EndProperty();
            }
        }

        /// <inheritdoc />
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) +
                   (_referenceApplied ? EditorGUIUtility.singleLineHeight : 0f);
        }
    }
}
#endif