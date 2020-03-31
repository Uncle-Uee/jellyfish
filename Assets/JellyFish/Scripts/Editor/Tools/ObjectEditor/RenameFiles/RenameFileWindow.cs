#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace JellyFish.EditorTools.ObjectEditor
{
    public partial class RenameFile : EditorWindow
    {
        #region UNITY EDITOR WINDOW

        /// <summary>
        /// RenameFile Window Instance
        /// </summary>
        private static RenameFile _window;

        private static void OpenWindow()
        {
            _window = GetWindow<RenameFile>("Filename");
            _window.minSize = new Vector2(320, 180);
            _window.Show();
        }

        #endregion
    }
}
#endif