// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using UnityEngine;

namespace JellyFish.Fading
{
    public class SpriteRendererFadable : Fadable
    {
	    /// <summary>
	    ///     The sprite renderer reference.
	    /// </summary>
	    public SpriteRenderer SpriteRenderer;

        /// <inheritdoc />
        protected override Color GetColour()
        {
            return SpriteRenderer.color;
        }

        /// <inheritdoc />
        public override void UpdateColour(Color colour, float percentage)
        {
            SpriteRenderer.color = colour;
        }

#if UNITY_EDITOR
        /// <summary>
        ///     Adds a Sprite Renderer Fadable to the scene.
        /// </summary>
        [UnityEditor.MenuItem("GameObject/SOFlow/Fading/Fadables/Add Sprite Renderer Fadable", false, 10)]
        public static void AddComponentToScene()
        {
            SpriteRenderer sprite = UnityEditor.Selection.activeGameObject?.GetComponent<SpriteRenderer>();

            if(sprite != null)
            {
                SpriteRendererFadable fadable = sprite.gameObject.AddComponent<SpriteRendererFadable>();
                fadable.SpriteRenderer = sprite;

                return;
            }

            GameObject _gameObject = new GameObject("Sprite Renderer Fadable", typeof(SpriteRendererFadable));

            if(UnityEditor.Selection.activeTransform != null)
            {
                _gameObject.transform.SetParent(UnityEditor.Selection.activeTransform);
            }

            UnityEditor.Selection.activeGameObject = _gameObject;
        }
#endif
    }
}