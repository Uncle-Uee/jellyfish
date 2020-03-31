// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using System;
using System.Reflection;
using SOFlow.Data.Primitives;
using Object = UnityEngine.Object;
using System.Collections.Generic;
using UltEvents;
using UnityEditor;

namespace SOFlow.Internal
{
    public static partial class SOFlowEditorUtilities
    {
        /// <summary>
        ///     The list of expanded field flags.
        /// </summary>
        private static readonly Dictionary<int, bool> _expandedFlags = new Dictionary<int, bool>();
        
        /// <summary>
        /// The list of numeric slider data.
        /// </summary>
        private static readonly Dictionary<int, NumericSliderData> _numericSliders = new Dictionary<int, NumericSliderData>();
        
        /// <summary>
        /// The text areas data.
        /// </summary>
        private static readonly Dictionary<int, TextAreaData> _textAreas = new Dictionary<int, TextAreaData>();

        /// <summary>
        /// The numeric slider data file name.
        /// </summary>
        private static readonly string _numericSlidersFile = "Numeric Sliders.data";
        
        /// <summary>
        /// The text areas data file name.
        /// </summary>
        private static readonly string _textAreasFile = "Text Areas.data";

        /// <summary>
        ///     Draws the item field.
        /// </summary>
        /// <param name="itemLabel"></param>
        /// <param name="item"></param>
        public static void DrawItemField(string itemLabel, object item)
        {
            if(item == null)
            {
                EditorGUILayout.LabelField("Null");

                return;
            }

            int itemHashCode = item.GetHashCode();

            if(!_expandedFlags.ContainsKey(itemHashCode)) _expandedFlags.Add(itemHashCode, false);

            EditorGUILayout.BeginHorizontal();

            if(DrawColourButton(_expandedFlags[itemHashCode] ? "↑" : "↓",
                                SOFlowEditorSettings.AcceptContextColour))
                _expandedFlags[itemHashCode] = !_expandedFlags[itemHashCode];

            EditorGUILayout.LabelField(itemLabel);

            EditorGUILayout.LabelField(item.GetType().Name);
            EditorGUILayout.EndHorizontal();

            if(_expandedFlags[itemHashCode]) DrawTertiaryLayer(() => DrawExpandedField(item));
        }

        /// <summary>
        ///     Draws the expanded field values.
        /// </summary>
        /// <param name="item"></param>
        private static void DrawExpandedField(object item)
        {
            FieldInfo[] fields = item.GetType().GetFields();

            foreach(FieldInfo field in fields)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(field.Name);

                object fieldValue = field.GetValue(item);

                EditorGUILayout.LabelField(fieldValue == null ? "Null" : fieldValue.ToString());

                EditorGUILayout.EndHorizontal();
            }
        }

        /// <summary>
        ///     Draws all non-serializable fields for the provided object.
        /// </summary>
        public static void DrawNonSerializableFields(object target)
        {
            FieldInfo[] fields = target.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach(FieldInfo field in fields)
            {
                object value = field.GetValue(target);

                if(value != null)
                {
                    Type valueType = value.GetType();

                    if(!(value is Object)       && !(value is string)   &&
                       !(value is DataField)    && !(value is UltEvent) &&
                       !valueType.IsGenericType &&
                       valueType.IsClass        && !valueType.IsArray)
                        DrawItemField(valueType.Name, value);
                }
            }
        }
    }
}
#endif