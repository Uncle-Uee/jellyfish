// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using SOFlow.Internal;
using UnityEngine;
using XNodeEditor;

namespace SOFlow.Data.Graphs
{
    /// <inheritdoc />
    [CustomNodeEditor(typeof(EntryNode))]
    public class EntryNodeEditor : NodeEditor
    {
        public override void OnBodyGUI()
        {
            SOFlowEditorUtilities.AdjustTextContrast(SOFlowEditorSettings.AcceptContextColour);

            base.OnBodyGUI();

            SOFlowEditorUtilities.RestoreTextContrast();
        }

        /// <inheritdoc />
        public override Color GetTint()
        {
            return SOFlowEditorSettings.AcceptContextColour;
        }
    }
}
#endif