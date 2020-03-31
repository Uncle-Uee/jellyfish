// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using SOFlow.Internal;
using UnityEditor;

namespace JellyFish.PlayerInput
{
    [CustomEditor(typeof(GameInputListener))]
    public class GameInputListenerEditor : SOFlowCustomEditor
    {
        protected override void DrawCustomInspector()
        {
            base.DrawCustomInspector();

            SOFlowEditorUtilities.DrawPrimaryLayer(() =>
                                               {
                                                   serializedObject.DrawProperty("AutoCheckGameInput");

                                                   SOFlowEditorUtilities
                                                      .DrawListComponentProperty(serializedObject,
                                                                                 serializedObject.FindProperty("Input"),
                                                                                 SOFlowEditorSettings.SecondaryLayerColour);
                                               });
        }
    }
}
#endif