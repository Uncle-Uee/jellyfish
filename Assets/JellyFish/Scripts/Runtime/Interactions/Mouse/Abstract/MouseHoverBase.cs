using UnityEngine;
using UnityEngine.EventSystems;

namespace JellyFish.Interactions.Mouse
{
    public class MouseHoverBase : MonoBehaviour, IMouseHover
    {
        /// <summary>
        /// On Pointer Enter
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            print("On Mouse Enter.");
        }

        /// <summary>
        /// On Pointer Exit
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerExit(PointerEventData eventData)
        {
            print("On Mouse Exit.");
        }
    }
}