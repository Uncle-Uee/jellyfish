// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using UnityEngine;

namespace JellyFish.Fading
{
    public class LightFadable : Fadable
    {
	    /// <summary>
	    ///     The light source to fade.
	    /// </summary>
	    public Light LightSource;

        /// <inheritdoc />
        protected override Color GetColour()
        {
            return LightSource.color;
        }

        /// <inheritdoc />
        public override void UpdateColour(Color colour, float percentage)
        {
            LightSource.color = colour;
        }

#if UNITY_EDITOR
        /// <summary>
        ///     Adds a Light Fadable to the scene.
        /// </summary>
        [UnityEditor.MenuItem("GameObject/SOFlow/Fading/Fadables/Add Light Fadable", false, 10)]
        public static void AddComponentToScene()
        {
            Light light = UnityEditor.Selection.activeGameObject?.GetComponent<Light>();

            if(light != null)
            {
                LightFadable fadable = light.gameObject.AddComponent<LightFadable>();
                fadable.LightSource = light;
                
                return;
            }

            GameObject _gameObject = new GameObject("Light Fadable", typeof(LightFadable));

            if(UnityEditor.Selection.activeTransform != null)
            {
                _gameObject.transform.SetParent(UnityEditor.Selection.activeTransform);
            }

            UnityEditor.Selection.activeGameObject = _gameObject;
        }
#endif
    }
}