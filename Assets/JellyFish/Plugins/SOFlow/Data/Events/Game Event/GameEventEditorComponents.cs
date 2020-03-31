// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

namespace SOFlow.Data.Events
{
    [InitializeOnLoad]
    public partial class GameEvent
    {
        /// <summary>
        ///     The event stack.
        /// </summary>
        [HideInInspector]
        public List<GameEventLog> EventStack = new List<GameEventLog>();

        static GameEvent()
        {
            EditorApplication.hierarchyWindowItemOnGUI += AddDraggedGameEventToHierarchy;
        }

        /// <summary>
        ///     Adds the dragged game event to a new <see cref="GameEventListener" /> in the scene.
        /// </summary>
        /// <param name="instanceID"></param>
        /// <param name="selectionRect"></param>
        private static void AddDraggedGameEventToHierarchy(int instanceID, Rect selectionRect)
        {
            if(Event.current.type == EventType.DragExited && selectionRect.Contains(Event.current.mousePosition))
            {
                DragAndDrop.AcceptDrag();

                foreach(Object @object in DragAndDrop.objectReferences)
                    if(@object is GameEvent)
                        AddGameEventToScene(@object as GameEvent);

                Event.current.Use();
            }
        }

        /// <summary>
        ///     Adds the specified game event to the scene.
        /// </summary>
        /// <param name="event"></param>
        public static void AddGameEventToScene(GameEvent @event)
        {
            GameObject        gameObject = new GameObject(@event.name);
            GameEventListener listener   = gameObject.AddComponent<GameEventListener>();

            listener.Events.Add(@event);

            if(Selection.activeTransform != null) gameObject.transform.SetParent(Selection.activeTransform);

            EditorGUIUtility.PingObject(gameObject);
        }

        [MenuItem("Assets/SOFlow/Search Game Event", true)]
        public static bool SearchGameEventValidation()
        {
            return Selection.activeObject is GameEvent;
        }

        [MenuItem("Assets/SOFlow/Search Game Event %#F")]
        public static void SearchGameEventScene()
        {
            SearchableEditorWindow hierarchy = null;

            SearchableEditorWindow[] windows =
                (SearchableEditorWindow[])Resources.FindObjectsOfTypeAll(typeof(SearchableEditorWindow));

            foreach(SearchableEditorWindow window in windows)
                if(window.GetType().ToString() == "UnityEditor.SceneHierarchyWindow")
                {
                    hierarchy = window;

                    break;
                }

            if(hierarchy == null) return;

            MethodInfo setSearchType =
                typeof(SearchableEditorWindow).GetMethod("SetSearchFilter",
                                                         BindingFlags.NonPublic | BindingFlags.Instance);

            object[] parameters =
            {
                Selection.activeObject.name, 1, false, false
            };

            setSearchType?.Invoke(hierarchy, parameters);
        }
    }
}
#endif