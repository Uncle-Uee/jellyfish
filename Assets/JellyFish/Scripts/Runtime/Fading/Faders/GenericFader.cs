// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System.Collections.Generic;
using UltEvents;
using UnityAsync;
using UnityEngine;

namespace JellyFish.Fading
{
    public class GenericFader : MonoBehaviour
    {
        /// <summary>
        ///     The fade curve.
        /// </summary>
        public AnimationCurve FadeCurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));

        /// <summary>
        ///     The faded colour.
        /// </summary>
        public Color FadedColour = Color.white;

        /// <summary>
        ///     The fade targets.
        /// </summary>
        public List<Fadable> FadeTargets = new List<Fadable>();

        /// <summary>
        ///     The fade time.
        /// </summary>
        public float FadeTime;

        /// <summary>
        ///     Event raised when the fading is completed.
        /// </summary>
        public UltEvent OnFadeComplete;

        /// <summary>
        ///     Event raised before the fade starts.
        /// </summary>
        public UltEvent OnFadeStart;

        /// <summary>
        ///     Event raised when waiting between fades.
        /// </summary>
        public UltEvent OnFadeWait;

        /// <summary>
        ///     Enable to only allow fading in.
        /// </summary>
        public bool OnlyFade;

        /// <summary>
        ///     The unfade curve.
        /// </summary>
        public AnimationCurve UnfadeCurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));

        /// <summary>
        ///     The unfaded colour.
        /// </summary>
        public Color UnfadedColour = Color.white;

        /// <summary>
        ///     The unfade time.
        /// </summary>
        public float UnfadeTime;

        /// <summary>
        ///     The wait between fades.
        /// </summary>
        public float WaitBetweenFades;

        /// <summary>
        ///     Indicates whether we are currently fading.
        /// </summary>
        public bool Fading { get; private set; }

        /// <summary>
        ///     Initiates the fade.
        /// </summary>
        public async void Fade()
        {
            if (!Fading && gameObject.activeInHierarchy)
            {
                OnFadeStart.Invoke();
                Fading = true;

                float startTime = Time.realtimeSinceStartup;
                float endTime = startTime + FadeTime;

                while (Time.realtimeSinceStartup < endTime)
                {
                    float percentage = (Time.realtimeSinceStartup - startTime) /
                                       (endTime                   - startTime);

                    foreach (Fadable fadable in FadeTargets)
                        fadable.OnUpdateColour(Color.Lerp(UnfadedColour, FadedColour, FadeCurve.Evaluate(percentage)),
                                               percentage);

                    await Await.NextUpdate();
                }

                foreach (Fadable fadable in FadeTargets) fadable.OnUpdateColour(FadedColour, 1f);

                if (!OnlyFade)
                {
                    OnFadeWait.Invoke();

                    await Await.Seconds(WaitBetweenFades);

                    Unfade();
                }
                else
                {
                    Fading = false;
                    OnFadeComplete.Invoke();
                }
            }
        }

        private async void Unfade()
        {
            float startTime = Time.realtimeSinceStartup;
            float endTime = startTime + UnfadeTime;

            while (Time.realtimeSinceStartup < endTime)
            {
                float percentage = (Time.realtimeSinceStartup - startTime) /
                                   (endTime                   - startTime);

                foreach (Fadable fadable in FadeTargets)
                    fadable.OnUpdateColour(Color.Lerp(FadedColour, UnfadedColour, UnfadeCurve.Evaluate(percentage)),
                                           percentage);

                await Await.NextUpdate();
            }

            foreach (Fadable fadable in FadeTargets) fadable.OnUpdateColour(UnfadedColour, 0f);

            Fading = false;
            OnFadeComplete.Invoke();
        }

#if UNITY_EDITOR
        /// <summary>
        ///     Adds a Generic Fader to the scene.
        /// </summary>
        [UnityEditor.MenuItem("GameObject/SOFlow/Fading/Faders/Add Generic Fader", false, 10)]
        public static void AddComponentToScene()
        {
            GameObject _gameObject = new GameObject("Generic Fader", typeof(GenericFader));

            if (UnityEditor.Selection.activeTransform != null)
            {
                _gameObject.transform.SetParent(UnityEditor.Selection.activeTransform);
            }

            UnityEditor.Selection.activeGameObject = _gameObject;
        }
#endif
    }
}