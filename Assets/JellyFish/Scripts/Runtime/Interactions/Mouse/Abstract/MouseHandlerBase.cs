using UnityEngine;
using UnityEngine.EventSystems;

namespace JellyFish.Interactions.Mouse
{
    public abstract class MouseHandlerBase : MonoBehaviour, IMouseClick, IMouseHover, IMouseDrag
    {
        /// <summary>
        /// On Pointer Click.
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerClick(PointerEventData eventData)
        {
        }

        /// <summary>
        /// On Pointer Down.
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerDown(PointerEventData eventData)
        {
        }

        /// <summary>
        /// On Pointer Up.
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerUp(PointerEventData eventData)
        {
        }

        /// <summary>
        /// On Pointer Enter.
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerEnter(PointerEventData eventData)
        {
        }

        /// <summary>
        /// On Pointer Exit.
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerExit(PointerEventData eventData)
        {
        }

        /// <summary>
        /// On Pointer Beginner Drag.
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
        }

        /// <summary>
        /// On Pointer Drag.
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnDrag(PointerEventData eventData)
        {
        }

        /// <summary>
        /// On Pointer End Drag.
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnEndDrag(PointerEventData eventData)
        {
        }
    }
}