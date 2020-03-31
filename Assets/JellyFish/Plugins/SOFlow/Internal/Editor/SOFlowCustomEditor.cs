// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using System;
using UnityEngine;
using SOFlow.Utilities;
using UnityEditor;

namespace SOFlow.Internal
{
    public class SOFlowCustomEditor : Editor, ICustomEditor
    {
        /// <summary>
        ///     Indicates whether this instance is a scriptable object.
        /// </summary>
        protected bool _isScriptableObject = true;

        /// <summary>
        ///     The cached scriptable object instance.
        /// </summary>
        protected ScriptableObject _scriptableObjectTarget;

        protected void Awake()
        {
            if(_isScriptableObject && !_scriptableObjectTarget)
            {
                try
                {
                    _scriptableObjectTarget = (ScriptableObject)target;
                    _isScriptableObject     = true;
                }
                catch
                {
                    _isScriptableObject = false;
                }
            }
        }

        public override void OnInspectorGUI()
        {
            OnCustomInspectorGUI();
        }

        /// <summary>
        ///     Handles drawing the custom inspector GUI.
        /// </summary>
        protected virtual void OnCustomInspectorGUI()
        {
            if(SOFlowEditorSettings.DrawDefaultInspectors)
            {
                DrawDefaultInspector();
            }
            else
            {
                Color originalTextColour = GUI.contentColor;
                GUI.contentColor = SOFlowEditorSettings.TextColour;

                DrawCustomInspector();
                DrawScriptableObjectFileHandlingFields();

                GUI.contentColor = originalTextColour;

                if(GUI.changed)
                {
                    serializedObject.ApplyModifiedProperties();
                    
                    EditorApplication.RepaintProjectWindow();
                }
            }
        }

        /// <summary>
        ///     Draws the custom inspector.
        /// </summary>
        protected virtual void DrawCustomInspector()
        {
            if(_isScriptableObject && _scriptableObjectTarget)
            {
                SOFlowEditorUtilities.DrawTertiaryLayer(() =>
                                                        {
                                                            if(SOFlowEditorUtilities.DrawColourButton($"Save Assets {(EditorUtility.IsDirty(target) ? "*" : "")}",
                                                                                                      SOFlowEditorSettings
                                                                                                         .AcceptContextColour,
                                                                                                      SOFlowStyles
                                                                                                         .Button))
                                                            {
                                                                AssetDatabase.SaveAssets();
                                                            }
                                                        });
            }
        }

        /// <summary>
        ///     Draws file handling fields for scriptable object instances.
        /// </summary>
        protected virtual void DrawScriptableObjectFileHandlingFields()
        {
            if(_isScriptableObject && _scriptableObjectTarget)
            {
                string objectClassName = _scriptableObjectTarget.GetType().Name;

                SOFlowEditorUtilities.DrawHorizontalColourLayer(SOFlowEditorSettings.TertiaryLayerColour,
                                                            () =>
                                                            {
                                                                if(SOFlowEditorUtilities
                                                                   .DrawColourButton($"Save {objectClassName} Data",
                                                                                     SOFlowEditorSettings
                                                                                        .AcceptContextColour))
                                                                    _scriptableObjectTarget
                                                                       .SaveJSON($"Save {objectClassName} Data",
                                                                                 $"New {objectClassName} Data");

                                                                if(SOFlowEditorUtilities
                                                                   .DrawColourButton($"Load {objectClassName} Data",
                                                                                     SOFlowEditorSettings
                                                                                        .AcceptContextColour))
                                                                    _scriptableObjectTarget
                                                                       .LoadJSON($"Load {objectClassName} Data",
                                                                                 () =>
                                                                                 {
                                                                                     AssetDatabase.SaveAssets();

                                                                                     // Reselect the target instance in order to refresh the inspector and display the loaded values.
                                                                                     Selection
                                                                                        .SetActiveObjectWithContext(target,
                                                                                                                    _scriptableObjectTarget);
                                                                                 });
                                                            });
            }
        }
    }
}
#endif