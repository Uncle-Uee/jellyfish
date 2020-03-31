// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace SOFlow.Internal
{
    public class SOFlowStylesEditor : EditorWindow
    {
        /// <summary>
        ///     The custom style label width.
        /// </summary>
        private readonly float _labelWidth = 160f;

        /// <summary>
        ///     The custom GUI skin.
        /// </summary>
        private GUISkin _editorSkin;

        /// <summary>
        ///     The custom styles scroll position.
        /// </summary>
        private Vector2 _scrollPosition;

        /// <summary>
        ///     The data path.
        /// </summary>
        private string _dataPath => Path.Combine("Assets", "JellyFish", "Plugins", "SOFlow", "Internal", "Editor");
        
        [MenuItem("SOFlow/Editor/Editor Styles")]
        public static void ShowWindow()
        {
            GetWindow<SOFlowStylesEditor>("SOFlow-Editor Styles");
        }

        private void OnEnable()
        {
            string guiSkinPath = Path.Combine(_dataPath, "SOFlow GUI Skin.guiskin");

            if(!File.Exists(guiSkinPath))
            {
                _editorSkin = CreateInstance<GUISkin>();

                AssetDatabase.CreateAsset(_editorSkin, guiSkinPath);
                AssetDatabase.SaveAssets();
            }
            else
            {
                _editorSkin = AssetDatabase.LoadAssetAtPath<GUISkin>(guiSkinPath);
            }
        }

        private void OnGUI()
        {
            DrawWindowGUI();
        }

        /// <summary>
        ///     Draws the window GUI.
        /// </summary>
        private void DrawWindowGUI()
        {
            SOFlowEditorUtilities.DrawPrimaryLayer(() =>
                                               {
                                                   _editorSkin =
                                                       (GUISkin)EditorGUILayout.ObjectField("GUI Styles Reference",
                                                                                            _editorSkin,
                                                                                            typeof(GUISkin), true);

                                                   DrawCustomStyles();

                                                   GUILayout.FlexibleSpace();

                                                   DrawStylesManagementButtons();
                                               });
        }

        /// <summary>
        ///     Draws the GUI skin custom styles list.
        /// </summary>
        private void DrawCustomStyles()
        {
            EditorGUILayout.BeginScrollView(_scrollPosition, false, false, GUIStyle.none, GUIStyle.none,
                                            SOFlowStyles.HelpBox);

            DrawGUIStyleHeaders();

            EditorGUILayout.EndScrollView();

            SOFlowEditorUtilities.DrawScrollViewColourLayer(SOFlowEditorSettings.SecondaryLayerColour, ref _scrollPosition,
                                                        () =>
                                                        {
                                                            foreach(GUIStyle style in _editorSkin.customStyles)
                                                                DrawGUIStyle(style);
                                                        });
        }

        /// <summary>
        ///     Draws the given GUI style headers.
        /// </summary>
        private void DrawGUIStyleHeaders()
        {
            SOFlowEditorUtilities.DrawHorizontalColourLayer(SOFlowEditorSettings.TertiaryLayerColour,
                                                        () =>
                                                        {
                                                            EditorGUILayout.LabelField("Style Name",
                                                                                       SOFlowStyles.BoldCenterLabel,
                                                                                       GUILayout.Width(_labelWidth));

                                                            EditorGUILayout.LabelField("Font Name",
                                                                                       SOFlowStyles.BoldCenterLabel,
                                                                                       GUILayout.Width(_labelWidth));

                                                            EditorGUILayout.LabelField("Font Size",
                                                                                       SOFlowStyles.BoldCenterLabel,
                                                                                       GUILayout.Width(_labelWidth));

                                                            EditorGUILayout.LabelField("Font Style",
                                                                                       SOFlowStyles.BoldCenterLabel,
                                                                                       GUILayout.Width(_labelWidth));

                                                            EditorGUILayout.LabelField("Alignment",
                                                                                       SOFlowStyles.BoldCenterLabel,
                                                                                       GUILayout.Width(_labelWidth));

                                                            EditorGUILayout.LabelField("Word Wrapped",
                                                                                       SOFlowStyles.BoldCenterLabel,
                                                                                       GUILayout.Width(_labelWidth));

                                                            EditorGUILayout.LabelField("Text Clipping",
                                                                                       SOFlowStyles.BoldCenterLabel,
                                                                                       GUILayout.Width(_labelWidth));

                                                            EditorGUILayout.LabelField("Fixed Width",
                                                                                       SOFlowStyles.BoldCenterLabel,
                                                                                       GUILayout.Width(_labelWidth));

                                                            EditorGUILayout.LabelField("Fixed Height",
                                                                                       SOFlowStyles.BoldCenterLabel,
                                                                                       GUILayout.Width(_labelWidth));

                                                            EditorGUILayout.LabelField("Width Stretched",
                                                                                       SOFlowStyles.BoldCenterLabel,
                                                                                       GUILayout.Width(_labelWidth));

                                                            EditorGUILayout.LabelField("Height Stretched",
                                                                                       SOFlowStyles.BoldCenterLabel,
                                                                                       GUILayout.Width(_labelWidth));
                                                        });
        }

        /// <summary>
        ///     Draws the given GUI style.
        /// </summary>
        /// <param name="style"></param>
        private void DrawGUIStyle(GUIStyle style)
        {
            SOFlowEditorUtilities.DrawHorizontalColourLayer(SOFlowEditorSettings.SecondaryLayerColour,
                                                        () =>
                                                        {
                                                            EditorGUILayout
                                                               .LabelField(string.IsNullOrEmpty(style.name) ? "Untitled" : style.name,
                                                                           SOFlowStyles.CenteredLabel,
                                                                           GUILayout.Width(_labelWidth));

                                                            EditorGUILayout.LabelField(style.font == null
                                                                                           ? "No Font"
                                                                                           : style.font.name,
                                                                                       SOFlowStyles.CenteredLabel,
                                                                                       GUILayout.Width(_labelWidth));

                                                            EditorGUILayout.LabelField(style.fontSize.ToString(),
                                                                                       SOFlowStyles.CenteredLabel,
                                                                                       GUILayout.Width(_labelWidth));

                                                            EditorGUILayout.LabelField(style.fontStyle.ToString(),
                                                                                       SOFlowStyles.CenteredLabel,
                                                                                       GUILayout.Width(_labelWidth));

                                                            EditorGUILayout.LabelField(style.alignment.ToString(),
                                                                                       SOFlowStyles.CenteredLabel,
                                                                                       GUILayout.Width(_labelWidth));

                                                            EditorGUILayout.LabelField(style.wordWrap
                                                                                           ? "Word Wrap"
                                                                                           : "No Word Wrap",
                                                                                       SOFlowStyles.CenteredLabel,
                                                                                       GUILayout.Width(_labelWidth));

                                                            EditorGUILayout.LabelField(style.clipping.ToString(),
                                                                                       SOFlowStyles.CenteredLabel,
                                                                                       GUILayout.Width(_labelWidth));

                                                            EditorGUILayout.LabelField(style.fixedWidth.ToString(),
                                                                                       SOFlowStyles.CenteredLabel,
                                                                                       GUILayout.Width(_labelWidth));

                                                            EditorGUILayout.LabelField(style.fixedHeight.ToString(),
                                                                                       SOFlowStyles.CenteredLabel,
                                                                                       GUILayout.Width(_labelWidth));

                                                            EditorGUILayout.LabelField(style.stretchWidth
                                                                                           ? "Width Stretch"
                                                                                           : "No Width Stretch",
                                                                                       SOFlowStyles.CenteredLabel,
                                                                                       GUILayout.Width(_labelWidth));

                                                            EditorGUILayout.LabelField(style.stretchHeight
                                                                                           ? "Height Stretch"
                                                                                           : "No Height Stretch",
                                                                                       SOFlowStyles.CenteredLabel,
                                                                                       GUILayout.Width(_labelWidth));
                                                        });
        }

        /// <summary>
        ///     Draws the styles management buttons.
        /// </summary>
        private void DrawStylesManagementButtons()
        {
            SOFlowEditorUtilities.DrawHorizontalColourLayer(SOFlowEditorSettings.SecondaryLayerColour,
                                                        () =>
                                                        {
                                                            if(SOFlowEditorUtilities.DrawColourButton("Import",
                                                                                                  SOFlowEditorSettings
                                                                                                     .TertiaryLayerColour)
                                                            )
                                                            {
                                                            }

                                                            GUILayout.FlexibleSpace();

                                                            if(SOFlowEditorUtilities.DrawColourButton("Save",
                                                                                                  SOFlowEditorSettings
                                                                                                     .AcceptContextColour)
                                                            )
                                                                SaveSOFlowStyles();
                                                        });
        }

        /// <summary>
        ///     Saves the SOFlow Styles to script.
        /// </summary>
        private void SaveSOFlowStyles()
        {
            string scriptTemplatePath = Path.Combine(_dataPath, "SOFlowStylesTemplate.txt");

            if(File.Exists(scriptTemplatePath))
            {
                StringBuilder scriptText     = new StringBuilder();
                string        styleTemplate  = "        public static GUIStyle #VARIABLE# => GetStyle(#KEY#);\r\n";
                string        scriptTemplate = File.ReadAllText(scriptTemplatePath);

                foreach(GUIStyle style in _editorSkin.customStyles)
                    if(!string.IsNullOrEmpty(style.name))
                        scriptText.Append(styleTemplate.Replace("#VARIABLE#", style.name.Replace(" ", ""))
                                                       .Replace("#KEY#", $"\"{style.name}\""));

                scriptTemplate = scriptTemplate.Replace("#STYLES#", scriptText.ToString());

                File.WriteAllText(Path.Combine(_dataPath, "SOFlowStyles.cs"), scriptTemplate);

                AssetDatabase.Refresh();
            }
            else
            {
                EditorUtility.DisplayDialog("Error",
                                            "Failed to save SOFlow Styles.\n\nSOFlow Styles script template unavailable", "OK");
            }
        }
    }
}
#endif