// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JellyFish.Fading
{
    public class FadableGroup : Fadable
    {
	    /// <summary>
	    ///     The list of fadables managed by his fadable group.
	    /// </summary>
	    public List<Fadable> Fadables = new List<Fadable>();

        /// <inheritdoc />
        protected override Color GetColour()
        {
            return default;
        }

        /// <inheritdoc />
        public override void UpdateColour(Color colour, float percentage)
        {
            foreach(Fadable fadable in Fadables) fadable.OnUpdateColour(colour, percentage);
        }

        /// <summary>
        ///     Captures all child fadables.
        /// </summary>
        public void CaptureChildFadables()
        {
            Fadables.Clear();

            List<Fadable> fadables = GetComponentsInChildren<Fadable>().ToList();
            fadables.Remove(this);

            Fadables.AddRange(fadables);
        }

#if UNITY_EDITOR
        /// <summary>
        ///     Adds a Fadable Group to the scene.
        /// </summary>
        [UnityEditor.MenuItem("GameObject/SOFlow/Fading/Fadables/Add Fadable Group", false, 10)]
        public static void AddComponentToScene()
        {
            if(UnityEditor.Selection.activeGameObject != null)
            {
                UnityEditor.Selection.activeGameObject.AddComponent<FadableGroup>();

                return;
            }

            GameObject _gameObject = new GameObject("Fadable Group", typeof(FadableGroup));

            if(UnityEditor.Selection.activeTransform != null)
            {
                _gameObject.transform.SetParent(UnityEditor.Selection.activeTransform);
            }

            UnityEditor.Selection.activeGameObject = _gameObject;
        }
#endif
    }
}