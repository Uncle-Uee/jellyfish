// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using SOFlow.Internal;
using XNode;
using XNodeEditor;
using UnityEngine;

namespace SOFlow.Data.Graphs
{
    /// <inheritdoc />
    [CustomNodeEditor(typeof(BranchNode))]
    public class BranchNodeEditor : TraversableNodeEditor
    {
        /// <inheritdoc />
        public override void OnBodyGUI()
        {
            SOFlowEditorUtilities.AdjustTextContrast(GetTint());

            serializedObject.Update();

            BranchNode branch = (BranchNode)target;

            NodePort entryPort   = GetDynamicPort("Entry",   true, Node.ConnectionType.Multiple);
            NodePort defaultPort = GetDynamicPort("Default", false);

            SOFlowEditorUtilities.DrawHorizontalColourLayer(SOFlowEditorSettings.TertiaryLayerColour,
                                                        () =>
                                                        {
                                                            NodeEditorGUILayout.PortField(entryPort);
                                                        });

            SOFlowEditorUtilities.RestoreTextContrast();

            NodeEditorGUILayout.DynamicPortList(nameof(branch.Conditions), typeof(Node), serializedObject,
                                                NodePort.IO.Output, Node.ConnectionType.Override);

            SOFlowEditorUtilities.DrawHorizontalColourLayer(SOFlowEditorSettings.TertiaryLayerColour,
                                                        () =>
                                                        {
                                                            NodeEditorGUILayout.PortField(defaultPort);
                                                        });

            serializedObject.ApplyModifiedProperties();
        }

        /// <inheritdoc />
        public override Color GetTint()
        {
            if(IsCurrentNode()) return base.GetTint();

            return SOFlowEditorSettings.StandardNodeColour;
        }
    }
}
#endif