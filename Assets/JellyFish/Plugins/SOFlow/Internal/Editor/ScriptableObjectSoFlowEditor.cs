// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace SOFlow.Internal
{
    [CustomEditor(typeof(ScriptableObject), true)]
    [CanEditMultipleObjects]
    public class ScriptableObjectSoFlowEditor : SOFlowCustomEditor
    {
        protected override void DrawCustomInspector()
        {
            base.DrawCustomInspector();

            SOFlowEditorUtilities.DrawLayeredProperties(serializedObject);

            SOFlowEditorUtilities.DrawTertiaryLayer(() =>
                                                {
                                                    SOFlowEditorUtilities.DrawNonSerializableFields(target);
                                                });
        }
    }
}
#endif