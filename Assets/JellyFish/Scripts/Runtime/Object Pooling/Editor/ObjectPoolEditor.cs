// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using System.Collections;
using System.Reflection;
using SOFlow.Internal;
using UnityEditor;

namespace JellyFish.ObjectPooling
{
    [CustomEditor(typeof(ObjectPoolBase), true)]
    public class ObjectPoolEditor : SOFlowCustomEditor
    {
        /// <summary>
        ///     The ObjectPool target.
        /// </summary>
        private ObjectPoolBase _target;

        private void OnEnable()
        {
            _target = (ObjectPoolBase) target;
        }

        protected override void DrawCustomInspector()
        {
            base.DrawCustomInspector();

            SOFlowEditorUtilities.DrawLayeredProperties(serializedObject);

            SOFlowEditorUtilities.DrawTertiaryLayer(() => { SOFlowEditorUtilities.DrawNonSerializableFields(target); });

            SOFlowEditorUtilities.DrawSecondaryLayer(() =>
            {
                IEnumerable pool = _target.GetPool();

                foreach (object objectSet in pool)
                {
                    EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.LabelField("ID", SOFlowStyles.BoldCenterLabel);

                    EditorGUILayout.LabelField($"Pool Count - {_target.CurrentPoolSize}",
                                               SOFlowStyles.BoldCenterLabel);

                    EditorGUILayout.EndHorizontal();

                    PropertyInfo key = objectSet.GetType().GetProperty("Key");
                    PropertyInfo value = objectSet.GetType().GetProperty("Value");

                    PropertyInfo valueCount = value
                                              .GetValue(objectSet).GetType()
                                              .GetProperty("Count");

                    SOFlowEditorUtilities
                        .DrawHorizontalColourLayer(SOFlowEditorSettings.TertiaryLayerColour,
                                                   () =>
                                                   {
                                                       EditorGUILayout
                                                           .LabelField(key.GetValue(objectSet).ToString(),
                                                                       SOFlowStyles
                                                                           .CenteredLabel);

                                                       EditorGUILayout
                                                           .LabelField(valueCount.GetValue(value.GetValue(objectSet)).ToString(),
                                                                       SOFlowStyles
                                                                           .CenteredLabel);
                                                   });
                }
            });
        }
    }
}
#endif