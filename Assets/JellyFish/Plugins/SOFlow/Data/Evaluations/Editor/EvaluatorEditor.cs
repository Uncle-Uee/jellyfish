// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using SOFlow.Internal;
using UnityEditor;
using UnityEngine;

namespace SOFlow.Data.Evaluations.Editor
{
    [CustomEditor(typeof(Evaluator))]
    public class EvaluatorEditor : SOFlowCustomEditor
    {
        /// <summary>
        ///     The result text style.
        /// </summary>
        private GUIStyle _resultStyle;

        /// <summary>
        ///     The Evaluator target.
        /// </summary>
        private Evaluator _target;

        /// <summary>
        ///     The evaluation test result.
        /// </summary>
        private bool _testResult;

        private void OnEnable()
        {
            _target = (Evaluator)target;
        }

        /// <inheritdoc />
        protected override void DrawCustomInspector()
        {
            base.DrawCustomInspector();

            SOFlowEditorUtilities.DrawPrimaryLayer(() =>
                                               {
                                                   SOFlowEditorUtilities
                                                      .DrawListComponentProperty(serializedObject,
                                                                                 serializedObject
                                                                                    .FindProperty("Comparisons"),
                                                                                 SOFlowEditorSettings.SecondaryLayerColour);

                                                   serializedObject.DrawProperty("Any");
                                               });

            SOFlowEditorUtilities.DrawPrimaryLayer(DrawEvaluationTest);
        }

        /// <summary>
        ///     Draws evaluation testing tools.
        /// </summary>
        private void DrawEvaluationTest()
        {
            Color originalGUIColor = GUI.backgroundColor;

            SOFlowEditorUtilities.DrawColourLayer(_testResult
                                                  ? SOFlowEditorSettings.AcceptContextColour
                                                  : SOFlowEditorSettings.DeclineContextColour,
                                              () =>
                                              {
                                                  if(SOFlowEditorUtilities.DrawColourButton("Test Comparison"))
                                                      _testResult = _target.Evaluate();

                                                  GUI.backgroundColor =
                                                      _testResult
                                                          ? SOFlowEditorSettings.AcceptContextColour
                                                          : SOFlowEditorSettings.DeclineContextColour;

                                                  GUILayout.Label(_testResult.ToString(), SOFlowStyles.BoldCenterLabel);
                                              });

            GUI.backgroundColor = originalGUIColor;
        }
    }
}
#endif