using UnityEngine.EventSystems;

namespace JellyFish.Interactions.Mouse
{
    /// <summary>
    /// Mouse Pointer Click Interactions such as Mouse Button Click, Mouse Button Down, Mouse Button Up
    /// </summary>
    public interface IMouseClick : IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
    }
}