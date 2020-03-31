// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using SOFlow.Data.Primitives;
using UltEvents;
using UnityEngine;

namespace SOFlow.Data.Events
{
    public class PhysicsGameEventReactor : MonoBehaviour
    {
        /// <summary>
        ///     Enable to listen for OnCollisionEnter events.
        /// </summary>
        public BoolField ListenForCollisionEnter = new BoolField();

        /// <summary>
        ///     Enable to listen for OnCollisionExit events.
        /// </summary>
        public BoolField ListenForCollisionExit = new BoolField();

        /// <summary>
        ///     Enable to listen for OnCollisionStay events.
        /// </summary>
        public BoolField ListenForCollisionStay = new BoolField();

        /// <summary>
        ///     Enable to listen for OnTriggerEnter events.
        /// </summary>
        public BoolField ListenForTriggerEnter = new BoolField();

        /// <summary>
        ///     Enable to listen for OnTriggerExit events.
        /// </summary>
        public BoolField ListenForTriggerExit = new BoolField();

        /// <summary>
        ///     Enable to listen for OnTriggerStay events.
        /// </summary>
        public BoolField ListenForTriggerStay = new BoolField();

        /// <summary>
        ///     The OnCollisionEnter game event.
        /// </summary>
        public UltEvent OnCollisionEnterEvent = new UltEvent();

        /// <summary>
        ///     The OnCollisionExit game event.
        /// </summary>
        public UltEvent OnCollisionExitEvent = new UltEvent();

        /// <summary>
        ///     The OnCollisionStay game event.
        /// </summary>
        public UltEvent OnCollisionStayEvent = new UltEvent();

        /// <summary>
        ///     The OnTriggerEnter game event.
        /// </summary>
        public UltEvent OnTriggerEnterEvent = new UltEvent();

        /// <summary>
        ///     The OnTriggerExit game event.
        /// </summary>
        public UltEvent OnTriggerExitEvent = new UltEvent();

        /// <summary>
        ///     The OnTriggerStay game event.
        /// </summary>
        public UltEvent OnTriggerStayEvent = new UltEvent();

        private void OnTriggerEnter(Collider other)
        {
            if(ListenForTriggerEnter) OnTriggerEnterEvent.Invoke();
        }

        private void OnTriggerStay(Collider other)
        {
            if(ListenForTriggerStay) OnTriggerStayEvent.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if(ListenForTriggerExit) OnTriggerExitEvent.Invoke();
        }

        private void OnCollisionEnter(Collision other)
        {
            if(ListenForCollisionEnter) OnCollisionEnterEvent.Invoke();
        }

        private void OnCollisionStay(Collision other)
        {
            if(ListenForCollisionStay) OnCollisionStayEvent.Invoke();
        }

        private void OnCollisionExit(Collision other)
        {
            if(ListenForCollisionExit) OnCollisionExitEvent.Invoke();
        }

#if UNITY_EDITOR
        /// <summary>
        ///     Adds a Physics Game Event Reactor to the scene.
        /// </summary>
        [UnityEditor.MenuItem("GameObject/SOFlow/Events/Add Physics Game Event Reactor", false, 10)]
        public static void AddComponentToScene()
        {
            if(UnityEditor.Selection.activeGameObject?.GetComponent<Collider>() != null)
            {
                UnityEditor.Selection.activeGameObject.AddComponent<PhysicsGameEventReactor>();

                return;
            }

            GameObject _gameObject = new GameObject("Physics Game Event Reactor", typeof(PhysicsGameEventReactor));

            if(UnityEditor.Selection.activeTransform != null)
            {
                _gameObject.transform.SetParent(UnityEditor.Selection.activeTransform);
            }

            UnityEditor.Selection.activeGameObject = _gameObject;
        }
#endif
    }
}