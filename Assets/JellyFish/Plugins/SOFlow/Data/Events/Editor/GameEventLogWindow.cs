// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using SOFlow.Internal;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace SOFlow.Data.Events.Editor
{
    public class GameEventLogWindow : EditorWindow
    {
        /// <summary>
        ///     The set of events being tracked by the log window.
        /// </summary>
        private static readonly Dictionary<int, GameEvent> Events = new Dictionary<int, GameEvent>();

        /// <summary>
        ///     The entry width.
        /// </summary>
        private readonly float _entryWidth = 150f;

        /// <summary>
        ///     The list of scroll positions for each game event entry.
        /// </summary>
        private readonly Dictionary<int, Vector2> _eventEntriesScrollPosition = new Dictionary<int, Vector2>();

        /// <summary>
        ///     The list of event entry search queries.
        /// </summary>
        private readonly Dictionary<int, string> _eventEntrySearchQuery = new Dictionary<int, string>();

        /// <summary>
        ///     The game event list scroll position.
        /// </summary>
        private Vector2 _eventListScrollPosition;

        /// <summary>
        ///     The game event search query.
        /// </summary>
        private string _searchQuery = "";

        [MenuItem("SOFlow/Game Event/Game Event Log")]
        public static void ShowWindow()
        {
            GetWindow<GameEventLogWindow>("SOFlow-Game Event Log");
        }

        private void OnGUI()
        {
            SOFlowEditorUtilities.DrawPrimaryLayer(() =>
                                               {
                                                   DrawHeaderTab();

                                                   SOFlowEditorUtilities
                                                      .DrawScrollViewColourLayer(SOFlowEditorSettings.PrimaryLayerColour,
                                                                                 ref _eventListScrollPosition,
                                                                                 DrawGameEventList);
                                               });
        }

        /// <summary>
        ///     Draws the header tab.
        /// </summary>
        private void DrawHeaderTab()
        {
            SOFlowEditorUtilities.DrawHorizontalColourLayer(SOFlowEditorSettings.PrimaryLayerColour,
                                                        () =>
                                                        {
                                                            if(SOFlowEditorUtilities.DrawColourButton("Clear All Logs",
                                                                                                  SOFlowEditorSettings
                                                                                                     .DeclineContextColour)
                                                            )
                                                                foreach(KeyValuePair<int, GameEvent> gameEvent in Events
                                                                )
                                                                    gameEvent.Value.EventStack.Clear();

                                                            GUILayout.FlexibleSpace();

                                                            EditorGUILayout.LabelField("Search:",
                                                                                       SOFlowStyles.WordWrappedMiniLabel);

                                                            _searchQuery = EditorGUILayout.TextField(_searchQuery);
                                                        });
        }

        /// <summary>
        ///     Draws the game event list.
        /// </summary>
        private void DrawGameEventList()
        {
            foreach(KeyValuePair<int, GameEvent> gameEvent in Events)
                if(string.IsNullOrEmpty(_searchQuery) ||
                   gameEvent.Value.name.ToLower().Contains(_searchQuery.ToLower()))
                    SOFlowEditorUtilities.DrawHorizontalColourLayer(SOFlowEditorSettings.SecondaryLayerColour,
                                                                () => DrawGameEventEntry(gameEvent.Value,
                                                                                         gameEvent.Key));
        }

        /// <summary>
        ///     Draws the given game event entry.
        /// </summary>
        /// <param name="gameEvent"></param>
        /// <param name="instanceID"></param>
        private void DrawGameEventEntry(GameEvent gameEvent, int instanceID)
        {
            SOFlowEditorUtilities.DrawSecondaryLayer(() =>
                                                 {
                                                     EditorGUILayout.ObjectField(gameEvent, typeof(GameEvent), true,
                                                                                 GUILayout.Width(_entryWidth));

                                                     if(SOFlowEditorUtilities.DrawColourButton("Raise",
                                                                                           SOFlowEditorSettings
                                                                                              .AcceptContextColour))
                                                         gameEvent.Raise();

                                                     if(SOFlowEditorUtilities.DrawColourButton("Clear Log",
                                                                                           SOFlowEditorSettings
                                                                                              .DeclineContextColour))
                                                         gameEvent.EventStack.Clear();

                                                     SOFlowEditorUtilities.DrawTertiaryLayer(() =>
                                                                                         {
                                                                                             DrawGameEventEntrySearchBar(instanceID);
                                                                                         });
                                                 }, GUILayout.MaxWidth(_entryWidth));

            DrawLogEntry(gameEvent, instanceID);
        }

        /// <summary>
        ///     Draws the search bar for the given game event entry.
        /// </summary>
        /// <param name="instanceID"></param>
        private void DrawGameEventEntrySearchBar(int instanceID)
        {
            EditorGUILayout.LabelField("Search", SOFlowStyles.CenterTextHelpBox);

            string searchQuery;

            if(!_eventEntrySearchQuery.TryGetValue(instanceID, out searchQuery))
            {
                searchQuery = "";
                _eventEntrySearchQuery.Add(instanceID, searchQuery);
            }

            searchQuery = EditorGUILayout.TextField(searchQuery);

            _eventEntrySearchQuery[instanceID] = searchQuery;
        }

        /// <summary>
        ///     Draws the log entry for the given game event.
        /// </summary>
        /// <param name="gameEvent"></param>
        /// <param name="instanceID"></param>
        private void DrawLogEntry(GameEvent gameEvent, int instanceID)
        {
            Vector2 scrollPosition;

            if(!_eventEntriesScrollPosition.TryGetValue(instanceID, out scrollPosition))
            {
                scrollPosition = Vector2.zero;
                _eventEntriesScrollPosition.Add(instanceID, scrollPosition);
            }

            Color originalGUIColor = GUI.backgroundColor;
            GUI.backgroundColor = SOFlowEditorSettings.TertiaryLayerColour;

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, true, false, GUI.skin.horizontalScrollbar,
                                                             GUIStyle.none, SOFlowStyles.HelpBox,
                                                             GUILayout.Height(EditorGUIUtility.singleLineHeight * 7f),
                                                             GUILayout.Width(position.width - _entryWidth - 40f));

            GUI.backgroundColor = originalGUIColor;

            EditorGUILayout.BeginHorizontal();

            for(int i = gameEvent.EventStack.Count - 1; i >= 0; i--)
            {
                int skippedEntries = 0;

                SOFlowEditorUtilities.DrawSecondaryLayer(() =>
                                                     {
                                                         EditorGUILayout.LabelField($"Log Entry {i + 1}",
                                                                                    SOFlowStyles.CenterTextHelpBox);

                                                         EditorGUILayout.BeginHorizontal();

                                                         for(int j = 0,
                                                                 condition = gameEvent.EventStack[i].Listener.Count;
                                                             j < condition;
                                                             j++)
                                                         {
                                                             string searchQuery;

                                                             _eventEntrySearchQuery.TryGetValue(instanceID,
                                                                                                out searchQuery);

                                                             GameEventLog log        = gameEvent.EventStack[i];
                                                             GameObject   gameObject = log.Listener[j].GetGameObject();

                                                             if(string.IsNullOrEmpty(searchQuery) ||
                                                                gameObject == null                ||
                                                                gameObject.name.ToLower()
                                                                          .Contains(searchQuery.ToLower()))
                                                                 SOFlowEditorUtilities
                                                                    .DrawColourLayer(log.IsError[j] ? SOFlowEditorSettings.DeclineContextColour : SOFlowEditorSettings.AcceptContextColour,
                                                                                     () =>
                                                                                         DrawEventLog(log, j));
                                                             else
                                                                 skippedEntries++;
                                                         }

                                                         EditorGUILayout.EndHorizontal();
                                                     },
                                                     GUILayout.MaxWidth(_entryWidth *
                                                                        (gameEvent.EventStack[i].Listener.Count -
                                                                         skippedEntries)));
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndScrollView();

            _eventEntriesScrollPosition[instanceID] = scrollPosition;
        }

        /// <summary>
        ///     Draws the given <see cref="GameEventLog" />.
        /// </summary>
        /// <param name="log"></param>
        /// <param name="index"></param>
        private void DrawEventLog(GameEventLog log, int index)
        {
            EditorGUILayout.LabelField(log.Listener[index].GetObjectType().Name, SOFlowStyles.BoldCenterLabel);
            EditorGUILayout.LabelField($"{log.LogTime:T}",                       SOFlowStyles.CenteredLabel);
            EditorGUILayout.ObjectField(log.Listener[index].GetGameObject(), typeof(GameObject), true);
        }

        /// <summary>
        ///     Logs the given event to the log window.
        /// </summary>
        /// <param name="gameEvent"></param>
        public static void LogEvent(GameEvent gameEvent)
        {
            int instanceID = gameEvent.GetInstanceID();

            if(!Events.ContainsKey(instanceID)) Events.Add(instanceID, gameEvent);
        }
    }
}
#endif