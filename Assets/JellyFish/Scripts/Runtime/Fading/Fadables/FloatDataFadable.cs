// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using SOFlow.Data.Primitives;
using UnityEngine;

namespace JellyFish.Fading
{
    public class FloatDataFadable : Fadable
    {
	    /// <summary>
	    ///     The float data to fade.
	    /// </summary>
	    public FloatField FloatData;

	    /// <summary>
	    ///     The fade range.
	    /// </summary>
	    public Vector2Field FadeRange;

        /// <inheritdoc />
        protected override Color GetColour()
        {
            return default;
        }

        /// <inheritdoc />
        public override void UpdateColour(Color colour, float percentage)
        {
            FloatData.Value = Mathf.Lerp(FadeRange.Value.x, FadeRange.Value.y, percentage);
        }

#if UNITY_EDITOR
        /// <summary>
        ///     Adds a Float Data Fadable to the scene.
        /// </summary>
        [UnityEditor.MenuItem("GameObject/SOFlow/Fading/Fadables/Add Float Data Fadable", false, 10)]
        public static void AddComponentToScene()
        {
	        GameObject _gameObject = new GameObject("Float Data Fadable", typeof(FloatDataFadable));

	        if(UnityEditor.Selection.activeTransform != null)
	        {
		        _gameObject.transform.SetParent(UnityEditor.Selection.activeTransform);
	        }

	        UnityEditor.Selection.activeGameObject = _gameObject;
        }
#endif
    }
}