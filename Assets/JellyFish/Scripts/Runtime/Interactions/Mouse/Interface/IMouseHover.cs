using UnityEngine.EventSystems;

namespace JellyFish.Interactions.Mouse
{
    /// <summary>
    /// Mouse Pointer Hover Interactions such as Mouse Pointer Enter and Mouse Pointer Exit
    /// </summary>
    public interface IMouseHover : IPointerEnterHandler, IPointerExitHandler
    {
    }
}