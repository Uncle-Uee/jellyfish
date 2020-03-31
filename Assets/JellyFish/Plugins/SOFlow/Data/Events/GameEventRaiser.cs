// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System;
using System.Collections;
using System.Threading;
using SOFlow.Data.Primitives;
using UltEvents;
using UnityAsync;
using UnityEngine;

namespace SOFlow.Data.Events
{
    public class GameEventRaiser : MonoBehaviour
    {
        /// <summary>
        ///     The game event to raise.
        /// </summary>
        public UltEvent Event = new UltEvent();

        /// <summary>
        ///     The time before the event is raised in seconds.
        /// </summary>
        public FloatField EventWaitTime = new FloatField();

        /// <summary>
        ///     Indicates whether the event should be raised automatically when Start is called.
        /// </summary>
        public BoolField RaiseOnStart = new BoolField(true);

        /// <summary>
        /// Indicates whether the event should be repeated.
        /// </summary>
        public BoolField RepeatEvent = new BoolField(false);

        /// <summary>
        /// The cancellation token source.
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private void Start()
        {
            if (RaiseOnStart) RaiseEvent();
        }

        /// <summary>
        ///     Raises this event after the specified period of time has passed.
        /// </summary>
        public async void RaiseEvent()
        {
            try
            {
                // Nullify any cancellation schedules as a new event has been invoked.
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = new CancellationTokenSource();
                Event.CancellationToken = _cancellationTokenSource.Token;

                await Await.Seconds(EventWaitTime).ConfigureAwait(_cancellationTokenSource.Token);

                Event.Invoke();

                if (RepeatEvent)
                {
                    RaiseEvent();
                }
            }
            catch (OperationCanceledException)
            {
                // Ignore a cancellation event.
            }
        }

        /// <summary>
        /// Cancels the event if it has been scheduled.
        /// </summary>
        public void CancelEvent()
        {
            _cancellationTokenSource.Cancel();
        }

#if UNITY_EDITOR
        /// <summary>
        ///     Adds a Game Event Raiser to the scene.
        /// </summary>
        [UnityEditor.MenuItem("GameObject/SOFlow/Events/Add Game Event Raiser", false, 10)]
        public static void AddComponentToScene()
        {
            GameObject _gameObject = new GameObject("Game Event Raiser", typeof(GameEventRaiser));

            if (UnityEditor.Selection.activeTransform != null)
            {
                _gameObject.transform.SetParent(UnityEditor.Selection.activeTransform);
            }

            UnityEditor.Selection.activeGameObject = _gameObject;
        }
#endif
    }
}