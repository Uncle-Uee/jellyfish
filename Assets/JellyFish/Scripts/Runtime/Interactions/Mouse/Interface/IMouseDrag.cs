using UnityEngine.EventSystems;

namespace JellyFish.Interactions.Mouse
{
    /// <summary>
    /// Mouse Pointer Drag Interactions such as Begin Dragging, Dragging and End Dragging.
    /// </summary>
    public interface IMouseDrag : IBeginDragHandler, IDragHandler, IEndDragHandler
    {
    }
}