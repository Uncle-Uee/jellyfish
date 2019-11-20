/**
 * Created By: Kearan Peterson
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;


#endif

// ReSharper disable once CheckNamespace
namespace JellyFish.Management.SceneSets
{
    [CreateAssetMenu(menuName = "JellyFish/Management/Scene/Scene Set")]
    public class SceneSet : ScriptableObject
    {
        /// <summary>
        /// The set scenes.
        /// </summary>
        public List<SceneField> SetScenes = new List<SceneField>();

        /// <summary>
        /// Loads this scene set according to the additive parameter.
        /// </summary>
        /// <param name="additive"></param>
        public void LoadSceneSet(bool additive)
        {
            bool firstScene = !additive;

            foreach (SceneField scene in SetScenes)
            {
                if (scene.SceneAsset || !string.IsNullOrEmpty(scene.SceneName))
                {
#if UNITY_EDITOR
                    if (Application.isPlaying)
                    {
                        SceneManager.LoadSceneAsync(scene, firstScene ? LoadSceneMode.Single : LoadSceneMode.Additive);
                    }
                    else
                    {
                        EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(scene.SceneAsset),
                                                     firstScene ? OpenSceneMode.Single : OpenSceneMode.Additive);
                    }
#else
                    SceneManager.LoadSceneAsync(scene, firstScene ? LoadSceneMode.Single : LoadSceneMode.Additive);
#endif
                    firstScene = false;
                }
            }
        }

        /// <summary>
        /// Unloads this scene set.
        /// </summary>
        public void UnloadSceneSet()
        {
            foreach (SceneField scene in SetScenes)
            {
                if (scene.SceneAsset || !string.IsNullOrEmpty(scene.SceneName))
                {
#if UNITY_EDITOR
                    if (Application.isPlaying)
                    {
                        SceneManager.UnloadSceneAsync(scene);
                    }
                    else
                    {
                        EditorSceneManager
                            .CloseScene(SceneManager.GetSceneByPath(AssetDatabase.GetAssetPath(scene.SceneAsset))
                                        , true);
                    }
#else
                        SceneManager.UnloadSceneAsync(scene);
#endif
                }
            }
        }
    }
}