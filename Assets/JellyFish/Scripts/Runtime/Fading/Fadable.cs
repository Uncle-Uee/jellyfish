// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace JellyFish.Fading
{
    public abstract class Fadable : MonoBehaviour
    {
        /// <summary>
        ///     Indicates whether only the alpha value of the provided colour should be used.
        /// </summary>
        public bool AlphaOnly;

        /// <summary>
        ///     Indicates whether the alpha should be inverted.
        /// </summary>
        public bool InvertAlpha;

        /// <summary>
        ///     Indicates whether the provided fade percentage should be inverted.
        /// </summary>
        public bool InvertPercentage;

        /// <summary>
        ///     Returns the colour component of this fadable.
        /// </summary>
        /// <returns></returns>
        protected abstract Color GetColour();

        /// <summary>
        ///     Callback received from the fader component.
        /// </summary>
        /// <param name="colour"></param>
        /// <param name="percentage"></param>
        public void OnUpdateColour(Color colour, float percentage)
        {
            if(AlphaOnly)
            {
                Color currentColour = GetColour();

                colour.r = currentColour.r;
                colour.g = currentColour.g;
                colour.b = currentColour.b;
            }

            if(InvertAlpha) colour.a = 1f - colour.a;

            UpdateColour(colour, InvertPercentage ? 1f - percentage : percentage);
        }

        /// <summary>
        ///     Updates the colour for this fadable object.
        /// </summary>
        /// <param name="colour"></param>
        /// <param name="percentage"></param>
        public abstract void UpdateColour(Color colour, float percentage);

#if UNITY_EDITOR
        /// <summary>
        ///     Unfades all fadables under the selected gameobject.
        /// </summary>
        [MenuItem("GameObject/SOFlow/Unfade Fadables", false, 10)]
        public static void UnfadeAllFadables()
        {
            GameObject activeGameObject = Selection.activeGameObject;

            if(activeGameObject != null)
            {
                Fadable[] fadables = activeGameObject.GetComponentsInChildren<Fadable>();

                foreach(Fadable fadable in fadables) fadable.OnUpdateColour(new Color(1f, 1f, 1f, 1f), 1f);
            }
        }

        /// <summary>
        ///     Fades all fadables under the selected gameobject.
        /// </summary>
        [MenuItem("GameObject/SOFlow/Fade Fadables", false, 10)]
        public static void FadeAllFadables()
        {
            GameObject activeGameObject = Selection.activeGameObject;

            if(activeGameObject != null)
            {
                Fadable[] fadables = activeGameObject.GetComponentsInChildren<Fadable>();

                foreach(Fadable fadable in fadables) fadable.OnUpdateColour(new Color(1f, 1f, 1f, 0f), 0f);
            }
        }
#endif
    }
}