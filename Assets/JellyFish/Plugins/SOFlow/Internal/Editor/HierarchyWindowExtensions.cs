#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SOFlow.Internal
{
    [InitializeOnLoad]
    public static class HierarchyWindowExtensions
    {
        /// <summary>
        /// The list of dirty objects.
        /// </summary>
        private static List<int> _dirtyObjects = new List<int>();
        
        /// <summary>
        /// The list of parent objects to add to the dirty list.
        /// </summary>
        private static List<int> _parentObjectsToAdd = new List<int>();
        
        static HierarchyWindowExtensions()
        {
            EditorApplication.hierarchyWindowItemOnGUI -= OnHierarchyWindowItemGUI;
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemGUI;

            EditorSceneManager.sceneSaved -= OnSceneSaved;
            EditorSceneManager.sceneSaved += OnSceneSaved;
                                                 
            Undo.postprocessModifications -= OnUndoPostProcessModification;
            Undo.postprocessModifications += OnUndoPostProcessModification;
        }

        /// <summary>
        /// Draws an asterisk in front of the hierarchy listing of a dirty object.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rect"></param>
        private static void OnHierarchyWindowItemGUI(int id, Rect rect)
        {
            Object hierarchyObject = EditorUtility.InstanceIDToObject(id);

            if(hierarchyObject)
            {
                foreach(int dirtyObjectID in _dirtyObjects)
                {
                    if(id == dirtyObjectID)
                    {
                        rect.x -= 25f;
                        rect.width = 10f;
                        
                        GUI.Label(rect, "*");
                        
                        GameObject sceneObject = hierarchyObject as GameObject;
                        Transform parentObject = sceneObject.transform.parent;

                        while(parentObject != null)
                        {
                            int rootID = parentObject.gameObject.GetInstanceID();
                    
                            if(!_dirtyObjects.Contains(rootID))
                            {
                                _parentObjectsToAdd.Add(rootID);
                            }

                            parentObject = parentObject.parent;
                        }
                    }
                }

                foreach(int rootObjectID in _parentObjectsToAdd)
                {
                    _dirtyObjects.Add(rootObjectID);
                }
                
                _parentObjectsToAdd.Clear();
            }
        }

        /// <summary>
        /// Registers a dirty object when a modification occurs for scene objects.
        /// </summary>
        /// <param name="modifications"></param>
        /// <returns></returns>
        private static UndoPropertyModification[] OnUndoPostProcessModification(UndoPropertyModification[] modifications)
        {
            foreach(UndoPropertyModification modification in modifications)
            {
                Component sceneComponent = modification.currentValue.target as Component;
                GameObject sceneObject = sceneComponent != null ? sceneComponent.gameObject : modification.currentValue.target as GameObject;
                
                if(sceneObject)
                {
                    int sceneObjectID = sceneObject.GetInstanceID();
                    
                    if(!_dirtyObjects.Contains(sceneObjectID))
                    {
                        _dirtyObjects.Add(sceneObjectID);
                        
                        EditorApplication.RepaintHierarchyWindow();
                    }
                }
            }

            return modifications;
        }

        /// <summary>
        /// Clears all objects in the dirty list for the scene that was saved.
        /// </summary>
        /// <param name="scene"></param>
        private static void OnSceneSaved(Scene scene)
        {
            for(int i = _dirtyObjects.Count - 1; i >= 0; i--)
            {
                int dirtyObjectID = _dirtyObjects[i];
                GameObject sceneObject = (EditorUtility.InstanceIDToObject(dirtyObjectID) as GameObject);

                if(sceneObject && sceneObject.scene.Equals(scene))
                {
                    _dirtyObjects.RemoveAt(i);
                }
            }
        }
    }
}
#endif