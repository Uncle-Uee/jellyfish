// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using SOFlow.Data.Primitives;
using UnityEngine;

namespace JellyFish.Fading
{
    public class MaterialFadable : Fadable
    {
        /// <summary>
        /// Indicates whether a renderer or material should be used as a reference.
        /// </summary>
        public BoolField UseRenderer = new BoolField(true);

        /// <summary>
        /// The target renderer to fade.
        /// </summary>
        public Renderer TargetRenderer;

        /// <summary>
        /// The target material to fade.
        /// </summary>
        public Material TargetMaterial;
        
        /// <summary>
        /// Indicates whether the material colour property should be manually specified.
        /// </summary>
        public BoolField OverrideColourProperty = new BoolField(false);
        
        /// <summary>
        /// The colour property to adjust.
        /// </summary>
        public StringField ColourProperty =  new StringField("_Color");
        
        /// <inheritdoc />
        protected override Color GetColour()
        {
            Material materialReference = UseRenderer ? TargetRenderer.sharedMaterial : TargetMaterial;
            
            return OverrideColourProperty ? materialReference.GetColor(ColourProperty) : materialReference.color;
        }

        /// <inheritdoc />
        public override void UpdateColour(Color colour, float percentage)
        {
            Material materialReference = UseRenderer ? TargetRenderer.sharedMaterial : TargetMaterial;

            if(OverrideColourProperty)
            {
                materialReference.SetColor(ColourProperty, colour);
            }
            else
            {
                materialReference.color = colour;
            }
        }

#if UNITY_EDITOR
        /// <summary>
        ///     Adds a Material Fadable to the scene.
        /// </summary>
        [UnityEditor.MenuItem("GameObject/SOFlow/Fading/Fadables/Add Material Fadable", false, 10)]
        public static void AddComponentToScene()
        {
            Renderer _renderer = UnityEditor.Selection.activeGameObject?.GetComponent<Renderer>();

            if(_renderer != null)
            {
                MaterialFadable fadable = _renderer.gameObject.AddComponent<MaterialFadable>();
                fadable.TargetRenderer = _renderer;

                return;
            }

            GameObject _gameObject = new GameObject("Material Fadable", typeof(MaterialFadable));

            if(UnityEditor.Selection.activeTransform != null)
            {
                _gameObject.transform.SetParent(UnityEditor.Selection.activeTransform);
            }

            UnityEditor.Selection.activeGameObject = _gameObject;
        }
#endif
    }
}