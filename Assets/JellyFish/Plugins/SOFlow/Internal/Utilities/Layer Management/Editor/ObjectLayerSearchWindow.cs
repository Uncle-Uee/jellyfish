// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEditor;
using UnityEngine;

namespace SOFlow.Internal.LayerManagement
{
    public class ObjectLayerSearchWindow : EditorWindow
    {
        /// <summary>
        ///     The list of all game objects found with matching layers.
        /// </summary>
        private readonly List<GameObject> _layerSearchResults = new List<GameObject>();

        /// <summary>
        ///     The layers to search for in the scene.
        /// </summary>
        private LayerMask _searchedLayers;

        /// <summary>
        ///     The amount of scrolling that has occurred in the search results scroll view.
        /// </summary>
        private Vector2 _searchResultsScrollValue = Vector2.zero;

        [MenuItem("SOFlow/Object History/Object Layer Searcher")]
        public static void ShowWindow()
        {
            GetWindow<ObjectLayerSearchWindow>("SOFlow-Object Layer Searcher");
        }

        private void OnGUI()
        {
            SOFlowEditorUtilities.DrawHorizontalColourLayer(SOFlowEditorSettings.PrimaryLayerColour, DrawSearchBar);

            if(_layerSearchResults.Count > 0)
            {
                _searchResultsScrollValue = EditorGUILayout.BeginScrollView(_searchResultsScrollValue);

                foreach(GameObject result in _layerSearchResults)
                    EditorGUILayout.ObjectField(result, typeof(GameObject), true);

                EditorGUILayout.EndScrollView();
            }
        }

        /// <summary>
        ///     Draws the search bar.
        /// </summary>
        private void DrawSearchBar()
        {
#if UNITY_EDITOR
            _searchedLayers =
                InternalEditorUtility
                   .ConcatenatedLayersMaskToLayerMask(EditorGUILayout
                                                         .MaskField(InternalEditorUtility.LayerMaskToConcatenatedLayersMask(_searchedLayers),
                                                                    InternalEditorUtility
                                                                       .layers));
#endif

            if(SOFlowEditorUtilities.DrawColourButton("Search", Color.cyan))
            {
                _layerSearchResults.Clear();

                if(_searchedLayers.value > 0)
                    foreach(GameObject result in
                        FindObjectsOfType<GameObject>())
                        if(_searchedLayers ==
                           (_searchedLayers | (1 << result.layer)))
                            _layerSearchResults.Add(result);
                else
                    _layerSearchResults.Clear();
            }
        }
    }
}
#endif