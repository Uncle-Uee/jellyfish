// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using SOFlow.Internal;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace SOFlow.Data.Events.Editor
{
    [CustomEditor(typeof(GameEvent))]
    public class GameEventEditor : SOFlowCustomEditor
    {
        /// <summary>
        ///     The current error data.
        /// </summary>
        private List<GameEventErrorData> _currentErrorData = new List<GameEventErrorData>();

        /// <summary>
        ///     The currently displayed error message.
        /// </summary>
        private string _currentErrorMessage = string.Empty;

        /// <summary>
        ///     The error scroll position.
        /// </summary>
        private Vector2 _errorScrollPosition;

        /// <summary>
        ///     The listeners scroll position.
        /// </summary>
        private Vector2 _listenersScrollPosition;

        /// <summary>
        ///     The log scroll position.
        /// </summary>
        private Vector2 _logScrollPosition;

        /// <summary>
        ///     The scroll height.
        /// </summary>
        private readonly float _scrollHeight = 200f;

        /// <summary>
        ///     The GameEvent target.
        /// </summary>
        private GameEvent _target;

        private void OnEnable()
        {
            _target = (GameEvent)target;
        }

        /// <inheritdoc />
        protected override void OnCustomInspectorGUI()
        {
            base.OnCustomInspectorGUI();

            if(GUI.changed) EditorUtility.SetDirty(_target);
        }

        /// <inheritdoc />
        protected override void DrawCustomInspector()
        {
            base.DrawCustomInspector();

            SOFlowEditorUtilities.DrawPrimaryLayer(() =>
                                               {
                                                   if(SOFlowEditorUtilities.DrawColourButton("Raise",
                                                                                         SOFlowEditorSettings
                                                                                            .AcceptContextColour))
                                                       _target.Raise();

                                                   if(SOFlowEditorUtilities.DrawColourButton("Search In Scene",
                                                                                         SOFlowEditorSettings
                                                                                            .TertiaryLayerColour))
                                                       SearchEventInScene();

                                                   if(SOFlowEditorUtilities.DrawColourButton("Add To Scene",
                                                                                         SOFlowEditorSettings
                                                                                            .TertiaryLayerColour))
                                                       GameEvent.AddGameEventToScene(_target);
                                               });

            SOFlowEditorUtilities.DrawSecondaryLayer(DrawEventListeners);
            SOFlowEditorUtilities.DrawTertiaryLayer(DrawEventStack);
            SOFlowEditorUtilities.DrawTertiaryLayer(DrawErrorMessage);
        }

        /// <summary>
        ///     Draws the list of event listeners.
        /// </summary>
        private void DrawEventListeners()
        {
            EditorGUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Listeners", SOFlowStyles.BoldCenterLabel);
            GUILayout.FlexibleSpace();

            EditorGUILayout.LabelField($"Size: {_target.Listeners.Count}", SOFlowStyles.WordWrappedMiniLabel);

            EditorGUILayout.EndHorizontal();

            SOFlowEditorUtilities.DrawScrollViewColourLayer(SOFlowEditorSettings.SecondaryLayerColour,
                                                        ref _listenersScrollPosition,
                                                        () =>
                                                        {
                                                            for(int index = 0; index < _target.Listeners.Count; index++)
                                                            {
                                                                IEventListener listener = _target.Listeners[index];

                                                                SOFlowEditorUtilities
                                                                   .DrawHorizontalColourLayer(SOFlowEditorSettings.TertiaryLayerColour,
                                                                                              () =>
                                                                                              {
                                                                                                  EditorGUILayout
                                                                                                     .LabelField($"{index} | {listener.GetObjectType().Name}");

                                                                                                  EditorGUILayout
                                                                                                     .ObjectField(listener.GetGameObject(),
                                                                                                                  typeof
                                                                                                                  (GameObject
                                                                                                                  ),
                                                                                                                  true);
                                                                                              });
                                                            }
                                                        }, GUILayout.MaxHeight(_scrollHeight));
        }

        /// <summary>
        ///     Draws the event stack.
        /// </summary>
        private void DrawEventStack()
        {
            EditorGUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Event Stack", SOFlowStyles.BoldCenterLabel);
            GUILayout.FlexibleSpace();

            EditorGUILayout.LabelField($"Size: {_target.EventStack.Count}", SOFlowStyles.WordWrappedMiniLabel);

            EditorGUILayout.EndHorizontal();

            SOFlowEditorUtilities.DrawScrollViewColourLayer(SOFlowEditorSettings.TertiaryLayerColour, ref _logScrollPosition,
                                                        () =>
                                                        {
                                                            foreach(GameEventLog log in _target.EventStack)
                                                                SOFlowEditorUtilities.DrawSecondaryLayer(() =>
                                                                                                     {
                                                                                                         for(int i = 0,
                                                                                                                 errorIndex
                                                                                                                     = 0,
                                                                                                                 condition
                                                                                                                     = log
                                                                                                                      .Listener
                                                                                                                      .Count;
                                                                                                             i <
                                                                                                             condition;
                                                                                                             i++)
                                                                                                         {
                                                                                                             SOFlowEditorUtilities
                                                                                                                .DrawHorizontalColourLayer(log.IsError[i] ? SOFlowEditorSettings.DeclineContextColour : SOFlowEditorSettings.SecondaryLayerColour,
                                                                                                                                           () =>
                                                                                                                                           {
                                                                                                                                               EditorGUILayout
                                                                                                                                                  .LabelField($"[{log.LogTime:T}] {log.Listener[i].GetObjectType().Name}");

                                                                                                                                               EditorGUILayout
                                                                                                                                                  .ObjectField(log
                                                                                                                                                              .Listener
                                                                                                                                                                   [i]
                                                                                                                                                              .GetGameObject(),
                                                                                                                                                               typeof
                                                                                                                                                               (GameObject
                                                                                                                                                               ),
                                                                                                                                                               true);

                                                                                                                                               if
                                                                                                                                               (log
                                                                                                                                                  .IsError
                                                                                                                                                       [i]
                                                                                                                                               )
                                                                                                                                                   if
                                                                                                                                                   (SOFlowEditorUtilities
                                                                                                                                                      .DrawColourButton("Log",
                                                                                                                                                                        SOFlowEditorSettings
                                                                                                                                                                           .TertiaryLayerColour)
                                                                                                                                                   )
                                                                                                                                                   {
                                                                                                                                                       _currentErrorMessage
                                                                                                                                                           = $"{log.ErrorMessages[errorIndex]}\n\n{log.StackTraces[errorIndex]}";

                                                                                                                                                       _currentErrorData
                                                                                                                                                           = log
                                                                                                                                                              .ErrorData
                                                                                                                                                                   [errorIndex];
                                                                                                                                                   }
                                                                                                                                           });

                                                                                                             if(log
                                                                                                                .IsError
                                                                                                                     [i]
                                                                                                             )
                                                                                                                 errorIndex
                                                                                                                     ++;
                                                                                                         }
                                                                                                     });
                                                        }, GUILayout.MaxHeight(_scrollHeight));
        }

        /// <summary>
        ///     Draws the currently viewed error message.
        /// </summary>
        private void DrawErrorMessage()
        {
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Error Log", SOFlowStyles.CenterTextHelpBox);
            GUILayout.FlexibleSpace();

            SOFlowEditorUtilities.DrawScrollViewColourLayer(SOFlowEditorSettings.DeclineContextColour, ref _errorScrollPosition,
                                                        () =>
                                                        {
                                                            EditorGUILayout.TextArea(_currentErrorMessage,
                                                                                     SOFlowStyles.Label);
                                                        });

            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("<<< Open Scripts In IDE >>>", SOFlowStyles.CenterTextHelpBox);
            GUILayout.FlexibleSpace();

            foreach(GameEventErrorData errorData in _currentErrorData)
                if(SOFlowEditorUtilities.DrawColourButton($"{errorData.ErrorMethod} >> {errorData.ErrorLine}",
                                                      SOFlowEditorSettings.SecondaryLayerColour))
                    InternalEditorUtility.OpenFileAtLineExternal(errorData.ErrorFile, errorData.ErrorLine);
        }

        /// <summary>
        ///     Searches for this event within the scene.
        /// </summary>
        private void SearchEventInScene()
        {
            List<Object> foundListeners = new List<Object>();

            for(int i = 0, condition = SceneManager.sceneCount; i < condition; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);

                if(scene.isLoaded)
                    foreach(GameObject gameObject in scene.GetRootGameObjects())
                    {
                        IEventListener[] listeners = gameObject.GetComponentsInChildren<IEventListener>(true);

                        foreach(IEventListener listener in listeners)
                            if(listener.GetEvents().Exists(@event => @event == _target))
                                foundListeners.Add(listener.GetGameObject());
                    }
            }

            if(foundListeners.Count > 0)
                Selection.objects = foundListeners.ToArray();
            else
                EditorUtility.DisplayDialog("Game Event Search",
                                            $"Game Event |{_target.name}| not found in open scenes.", "OK");
        }
    }
}
#endif