// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using SOFlow.Internal;
using UnityEngine;
using UnityEditor;

namespace JellyFish.ObjectPooling
{
    [CustomEditor(typeof(PoolObjectListReference))]
    public class PoolObjectListReferenceEditor : SOFlowCustomEditor
    {
        /// <summary>
        /// The PoolObjectListReference target.
        /// </summary>
        public PoolObjectListReference _target;

        public void OnEnable()
        {
            _target = (PoolObjectListReference) target;
        }

        /// <inheritdoc />
        protected override void DrawCustomInspector()
        {
            base.DrawCustomInspector();

            SOFlowEditorUtilities.DrawPrimaryLayer(() =>
            {
                SOFlowEditorUtilities
                    .DrawHorizontalColourLayer(SOFlowEditorSettings.SecondaryLayerColour,
                                               () =>
                                               {
                                                   EditorGUILayout.LabelField("Total Pool Objects",
                                                                              SOFlowStyles
                                                                                  .Label);

                                                   EditorGUILayout.LabelField(_target.PoolObjectCount.ToString(),
                                                                              SOFlowStyles
                                                                                  .BoldLeftLabel);
                                               });

                SOFlowEditorUtilities.DrawSecondaryLayer(() =>
                {
                    foreach (IPoolObjectRoot poolObject in _target.PoolObjects)
                    {
                        EditorGUILayout.ObjectField(poolObject.GetObjectInstance(), typeof(Object), false);
                    }
                });
            });
        }
    }
}
#endif