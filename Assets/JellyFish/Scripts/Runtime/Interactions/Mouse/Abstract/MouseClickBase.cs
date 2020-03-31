using UnityEngine;
using UnityEngine.EventSystems;

namespace JellyFish.Interactions.Mouse
{
    public class MouseClickBase : MonoBehaviour, IMouseClick
    {
        /// <summary>
        /// On Pointer Click
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                print("Left Mouse Button!");
            }

            if (eventData.button == PointerEventData.InputButton.Middle)
            {
                print("Middle Mouse Button!");
            }

            if (eventData.button == PointerEventData.InputButton.Right)
            {
                print("Right Mouse Button!");
            }
        }

        /// <summary>
        /// On Pointer Down
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            print("Down");
        }

        /// <summary>
        /// On Pointer Up
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnPointerUp(PointerEventData eventData)
        {
            print("Up");
        }
    }
}