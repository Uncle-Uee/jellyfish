// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SOFlow.Internal
{
    public static class SOFlowStyles
    {
        // The available styles.
        public static GUIStyle Label => GetStyle("Label");
        public static GUIStyle CenteredLabel => GetStyle("CenteredLabel");
        public static GUIStyle BoldLeftLabel => GetStyle("BoldLeftLabel");
        public static GUIStyle BoldCenterLabel => GetStyle("BoldCenterLabel");
        public static GUIStyle BoldFieldCenteredLabel => GetStyle("BoldFieldCenteredLabel");
        public static GUIStyle BoldTextField => GetStyle("BoldTextField");
        public static GUIStyle WordWrappedLabel => GetStyle("WordWrappedLabel");
        public static GUIStyle WordWrappedMiniLabel => GetStyle("WordWrappedMiniLabel");
        public static GUIStyle CenterTextHelpBox => GetStyle("CenterTextHelpBox");
        public static GUIStyle HelpBox => GetStyle("HelpBox");
        public static GUIStyle TextArea => GetStyle("TextArea");
        public static GUIStyle Button => GetStyle("Button");
        public static GUIStyle PressedButton => GetStyle("PressedButton");
        public static GUIStyle BoldCenterTextHelpBox => GetStyle("BoldCenterTextHelpBox");
        public static GUIStyle HeaderHelpBox => GetStyle("HeaderHelpBox");
        public static GUIStyle IconButton => GetStyle("IconButton");
        public static GUIStyle PressedIconButton => GetStyle("PressedIconButton");
        public static GUIStyle ButtonSmallText => GetStyle("ButtonSmallText");

        /// <summary>
        /// The GUI styles.
        /// </summary>
        private static GUISkin Styles
        {
            get
            {
                if(_styles == null)
                {
                    _styles = AssetDatabase.LoadAssetAtPath<GUISkin>(Path.Combine("Assets", "JellyFish", "Plugins",
                                                                                 "SOFlow", "Internal", "Editor",
                                                                                 "SOFlow GUI Skin.guiskin"));
                    
                    // Assets/JellyFish/Plugins/SOFlow/Internal/Editor/SOFlow GUI Skin.guiskin
                }

                return _styles;
            }
        }

        /// <summary>
        /// The GUI styles.
        /// </summary>
        private static GUISkin _styles;

        /// <summary>
        /// Gets the style for the given key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static GUIStyle GetStyle(string key)
        {
            foreach(GUIStyle style in Styles.customStyles)
            {
                if(style.name == key)
                {
                    return style;
                }
            }

            return GUIStyle.none;
        }
    }
}
#endif