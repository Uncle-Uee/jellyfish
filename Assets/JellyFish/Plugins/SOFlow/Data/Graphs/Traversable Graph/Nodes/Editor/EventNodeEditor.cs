// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using SOFlow.Internal;
using XNode;
using XNodeEditor;
using UnityEngine;

namespace SOFlow.Data.Graphs
{
    /// <inheritdoc />
    [CustomNodeEditor(typeof(EventNode))]
    public class EventNodeEditor : TraversableNodeEditor
    {
        /// <inheritdoc />
        public override void OnBodyGUI()
        {
            SOFlowEditorUtilities.AdjustTextContrast(GetTint());

            serializedObject.Update();

            EventNode eventNode = (EventNode)target;

            NodePort entryPort = GetDynamicPort("Entry", true, Node.ConnectionType.Multiple);
            NodePort exitPort  = GetDynamicPort("Exit",  false);

            SOFlowEditorUtilities.DrawHorizontalColourLayer(SOFlowEditorSettings.TertiaryLayerColour,
                                                        () =>
                                                        {
                                                            NodeEditorGUILayout.PortField(entryPort,
                                                                                          GUILayout.MinWidth(0f));

                                                            NodeEditorGUILayout.PortField(exitPort,
                                                                                          GUILayout.MinWidth(0f));
                                                        });

            SOFlowEditorUtilities.RestoreTextContrast();

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(eventNode.Event)));

            serializedObject.ApplyModifiedProperties();
        }

        /// <inheritdoc />
        public override Color GetTint()
        {
            if(IsCurrentNode()) return base.GetTint();

            return SOFlowEditorSettings.SecondaryLayerColour;
        }
    }
}
#endif