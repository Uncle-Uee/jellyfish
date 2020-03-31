// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using SOFlow.Internal;
using UnityEditor;

namespace JellyFish.Fading
{
    [CustomEditor(typeof(FadableGroup))]
    public class FadableGroupEditor : SOFlowCustomEditor
    {
	    /// <summary>
	    ///     The FadableGroup target.
	    /// </summary>
	    private FadableGroup _target;

        private void OnEnable()
        {
            _target = (FadableGroup)target;
        }

        /// <inheritdoc />
        protected override void DrawCustomInspector()
        {
            base.DrawCustomInspector();

            SOFlowEditorUtilities.DrawTertiaryLayer(() =>
                                                {
                                                    if(SOFlowEditorUtilities.DrawColourButton("Capture Child Fadables",
                                                                                          SOFlowEditorSettings
                                                                                             .AcceptContextColour))
                                                        _target.CaptureChildFadables();
                                                });

            SOFlowEditorUtilities.DrawLayeredProperties(serializedObject);
        }
    }
}
#endif