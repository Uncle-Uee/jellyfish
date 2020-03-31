// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using System.Reflection;
using SOFlow.Internal;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace SOFlow.Data.Collections
{
    [CustomEditor(typeof(DataRuntimeSet))]
    public class DataRuntimeSetEditor : SOFlowCustomEditor
    {
        /// <summary>
        ///     The list of items from the DataRuntimeSet target.
        /// </summary>
        private List<object> _items = new List<object>();

        /// <summary>
        ///     The DataRuntimeSet target.
        /// </summary>
        private DataRuntimeSet _target;

        private void OnEnable()
        {
            _target = (DataRuntimeSet)target;

            try
            {
                _items = _target.GetType().GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance)
                                .GetValue(_target) as List<object>;

                if(_items == null) _items = new List<object>();
            }
            catch
            {
                _items = new List<object>();
            }
        }

        /// <inheritdoc />
        protected override void DrawCustomInspector()
        {
            base.DrawCustomInspector();

            SOFlowEditorUtilities.DrawPrimaryLayer(() =>
                                               {
                                                   DrawDefaultInspector();
                                                   SOFlowEditorUtilities.DrawSecondaryLayer(DrawListItems);
                                               });
        }

        /// <summary>
        ///     Draws the list items.
        /// </summary>
        private void DrawListItems()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            EditorGUILayout.LabelField("Items", SOFlowStyles.BoldCenterLabel);

            GUILayout.FlexibleSpace();

            EditorGUILayout.LabelField($"Size: {_items.Count}", SOFlowStyles.WordWrappedMiniLabel);
            EditorGUILayout.EndHorizontal();

            for(int i = 0; i < _items.Count; i++)
                SOFlowEditorUtilities.DrawTertiaryLayer(() =>
                                                    {
                                                        SOFlowEditorUtilities.DrawItemField($"Entry {i + 1}", _items[i]);
                                                    });
        }
    }
}
#endif