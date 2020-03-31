// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using System.Globalization;
using SOFlow.Internal;
using UnityEditor;
using UnityEngine;

namespace SOFlow.Data.Evaluations.Editor
{
    [CustomEditor(typeof(GrowthCurve))]
    public class GrowthCurveEditor : SOFlowCustomEditor
    {
        /// <summary>
        ///     The growth curve bounds.
        /// </summary>
        private readonly Rect _growthCurveBounds = new Rect(0f, 0f, 1f, 1f);

        /// <summary>
        ///     The growth curve inspector dimensions.
        /// </summary>
        private Rect _growthCurveDimensions = Rect.zero;

        /// <summary>
        ///     The growth curve inspector height.
        /// </summary>
        private readonly int _growthCurveHeight = 5;

        /// <summary>
        ///     The GrowthCurve target.
        /// </summary>
        private GrowthCurve _target;

        private void OnEnable()
        {
            _target = (GrowthCurve)target;
        }

        /// <inheritdoc />
        protected override void DrawCustomInspector()
        {
            base.DrawCustomInspector();

            GUILayout.Space(5f);

            SOFlowEditorUtilities.DrawPrimaryLayer(() =>
                                               {
                                                   DrawCurve();

                                                   GUILayout.Space(10f);

                                                   DrawDataFields();

                                                   GUILayout.Space(10f);

                                                   DrawEvaluationArea();
                                               });
        }

        /// <summary>
        ///     Draws the growth curve.
        /// </summary>
        private void DrawCurve()
        {
            _growthCurveDimensions = new Rect(EditorGUILayout.GetControlRect())
                                     {
                                         height = EditorGUIUtility.singleLineHeight * _growthCurveHeight
                                     };

            _target.Curve = EditorGUI.CurveField(_growthCurveDimensions, _target.Curve,
                                                 SOFlowEditorSettings.TertiaryLayerColour,
                                                 _growthCurveBounds);

            EditorGUI.DrawRect(new Rect(_growthCurveDimensions.width * _target.CurveValue.Value +
                                        _growthCurveDimensions.x, _growthCurveDimensions.y, 2f,
                                        _growthCurveDimensions.height),
                               SOFlowEditorSettings.AcceptContextColour);

            // Add some spacing for the size of the growth curve.
            for(int i = 0; i < _growthCurveHeight - 1; i++) EditorGUILayout.LabelField("");

            _target.CurveValue.Value = GUILayout.HorizontalSlider(_target.CurveValue.Value, 0f, 1f);
        }

        /// <summary>
        ///     Draws the data fields.
        /// </summary>
        private void DrawDataFields()
        {
            serializedObject.DrawProperty("CurveValue");
            serializedObject.DrawProperty("EvaluatedValue");
            serializedObject.DrawProperty("GrowthMultiplier");
        }

        /// <summary>
        ///     Draws the test evaluation results.
        /// </summary>
        private void DrawEvaluationArea()
        {
            SOFlowEditorUtilities.DrawSecondaryLayer(() =>
                                                 {
                                                     _target.LiveEvaluation =
                                                         EditorGUILayout.Toggle("Live Evaluation",
                                                                                _target.LiveEvaluation);

                                                     if(_target.LiveEvaluation)
                                                     {
                                                         EditorGUILayout.BeginVertical(SOFlowStyles.HelpBox);

                                                         EditorGUILayout
                                                            .LabelField(_target.Evaluate().ToString(CultureInfo.InvariantCulture),
                                                                        EditorStyles.toolbarButton);

                                                         EditorGUILayout.EndVertical();
                                                     }
                                                     else
                                                     {
                                                         SOFlowEditorUtilities.DrawTertiaryLayer(() =>
                                                                                             {
                                                                                                 if(GUILayout
                                                                                                    .Button("Evaluate"))
                                                                                                     _target.Evaluate();

                                                                                                 EditorGUILayout
                                                                                                    .LabelField(_target.EvaluatedValue.Value.ToString(CultureInfo.InvariantCulture),
                                                                                                                EditorStyles
                                                                                                                   .toolbarButton);
                                                                                             });
                                                     }
                                                 });
        }
    }
}
#endif