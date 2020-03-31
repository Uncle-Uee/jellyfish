// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using SOFlow.Data.Primitives;
using UnityEngine;

namespace SOFlow.Data.Evaluations
{
    public class GrowthCurve : MonoBehaviour
    {
        /// <summary>
        ///     The growth curve.
        /// </summary>
        public AnimationCurve Curve = new AnimationCurve();

        /// <summary>
        ///     The value to feed into the curve.
        /// </summary>
        public FloatField CurveValue = new FloatField();

        /// <summary>
        ///     The resulting value after the growth curve has been applied.
        /// </summary>
        public FloatField EvaluatedValue = new FloatField();

        /// <summary>
        ///     The multiplier to apply to the growth curve.
        /// </summary>
        public FloatField GrowthMultiplier = new FloatField
                                             {
                                                 Value = 1f
                                             };

        /// <summary>
        ///     Enable to evaluate the growth curve every editor update.
        /// </summary>
        [HideInInspector]
        public bool LiveEvaluation;

        /// <summary>
        ///     Evaluates the curve value on the growth curve and updates the evaluated value accordingly.
        /// </summary>
        public float Evaluate()
        {
            EvaluatedValue.Value = Curve.Evaluate(CurveValue.Value) * GrowthMultiplier;

            return EvaluatedValue.Value;
        }

#if UNITY_EDITOR
        /// <summary>
        ///     Adds a Growth Curve to the scene.
        /// </summary>
        [UnityEditor.MenuItem("GameObject/SOFlow/Evaluations/Add Growth Curve", false, 10)]
        public static void AddComponentToScene()
        {
            GameObject _gameObject = new GameObject("Growth Curve", typeof(GrowthCurve));

            if(UnityEditor.Selection.activeTransform != null)
            {
                _gameObject.transform.SetParent(UnityEditor.Selection.activeTransform);
            }

            UnityEditor.Selection.activeGameObject = _gameObject;
        }
#endif
    }
}