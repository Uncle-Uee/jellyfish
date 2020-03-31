using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JellyFish.Interactions.Mouse
{
    public class MouseHandler : MouseHandlerBase
    {
        #region VARIABLES

        /// <summary>
        /// Target Position
        /// </summary>
        public Vector2 TargetPosition;

        #endregion

        #region METHODS

        /// <inheritdoc />
        public override void OnPointerClick(PointerEventData eventData)
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

        /// <inheritdoc />
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
        }

        /// <inheritdoc />
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
        }

        /// <inheritdoc />
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
        }

        /// <inheritdoc />
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
        }

        /// <inheritdoc />
        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
        }

        /// <inheritdoc />
        public override void OnDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Middle)
            {
                TargetPosition = Camera.main.ScreenToWorldPoint(eventData.position);
                transform.position = TargetPosition;
            }
        }

        /// <inheritdoc />
        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
        }

        #endregion
    }
}