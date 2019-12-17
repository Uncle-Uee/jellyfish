/**
 * Created by: Kearan Petersen
 * Blog: https://www.blumalice.wordpress.com
 * LinkedIn: https://www.linkedin.com/in/kearan-petersen/
 */


using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.Management.SceneSets
{
    [CreateAssetMenu(menuName = "JellyFish/Management/Scene/Scene Set Reference")]
    public class SceneSetReference : ScriptableObject
    {
        /// <summary>
        /// The scene set reference.
        /// </summary>
        public SceneSet SceneSet;

        /// <summary>
        /// Sets the scene set reference.
        /// </summary>
        /// <param name="sceneSet"></param>
        public void SetSceneSet(SceneSet sceneSet)
        {
            SceneSet = sceneSet;
        }

        /// <summary>
        /// Sets the scene set reference.
        /// </summary>
        /// <param name="sceneSetReference"></param>
        public void SetSceneSet(SceneSetReference sceneSetReference)
        {
            SceneSet = sceneSetReference.SceneSet;
        }

        /// <summary>
        /// Loads the referenced scene set.
        /// </summary>
        /// <param name="additive"></param>
        public void LoadSceneSet(bool additive)
        {
            if (SceneSet != null)
            {
                SceneSet.LoadSceneSet(additive);
            }
        }

        /// <summary>
        /// Unloads the referenced scene set.
        /// </summary>
        public void UnloadSceneSet()
        {
            if (SceneSet != null)
            {
                SceneSet.UnloadSceneSet();
            }
        }
    }
}