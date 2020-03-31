using UnityEngine;
using UnityEngine.EventSystems;

namespace JellyFish.Interactions.Mouse
{
    public class MouseDragBase : MonoBehaviour, IMouseDrag
    {
        /// <summary>
        /// On Begin Drag
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            print("On Begin Drag");
        }

        /// <summary>
        /// On Drag
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnDrag(PointerEventData eventData)
        {
            print("On Drag");
        }

        /// <summary>
        /// On End Drag
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnEndDrag(PointerEventData eventData)
        {
            print("On End Drag");
        }
    }
}