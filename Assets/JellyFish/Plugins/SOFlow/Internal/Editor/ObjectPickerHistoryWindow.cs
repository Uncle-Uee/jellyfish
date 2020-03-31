// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System.IO;
using System.Text;
#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using SOFlow.Extensions;
using Object = UnityEngine.Object;
using UnityEngine;
using UnityEditor;

namespace SOFlow.Internal
{
    public class ObjectPickerHistoryWindow : EditorWindow
    {
        /// <summary>
        /// THe list of objects that have been selected.
        /// </summary>
        public static readonly List<Object> ObjectPickerHistoryObjects = new List<Object>();

        /// <summary>
        /// The property that is currently being edited.
        /// </summary>
        public static SerializedProperty EditingProperty
        {
            get => _editingProperty;
            set
            {
                _editingProperty = value;
                UpdateObjectListing();
            }
        }

        /// <summary>
        /// Indicates whether the property is available or has been disposed.
        /// </summary>
        public static bool PropertyAvailable
        {
            get
            {
                try
                {
                    return EditingProperty?.propertyPath != null;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// The current listing type.
        /// </summary>
        public static string CurrentListingType;

        /// <summary>
        /// Indicates whether the window is currently open.
        /// </summary>
        public static bool IsOpen
        {
            get;
            private set;
        }

        /// <summary>
        /// The object picker history window.
        /// </summary>
        public static ObjectPickerHistoryWindow Window
        {
            get
            {
                if(_window == null)
                {
                    _window = GetWindow<ObjectPickerHistoryWindow>("SOFlow-Object Picker History");
                }

                return _window;
            }
        }

        /// <summary>
        /// The object picker history window.
        /// </summary>
        private static ObjectPickerHistoryWindow _window;

        /// <summary>
        /// The scroll position.
        /// </summary>
        private Vector2 _scrollPosition = Vector2.zero;

        /// <summary>
        /// Indicates whether a selected object is currently being monitored.
        /// </summary>
        private static bool _monitoringSelectedObject = false;

        /// <summary>
        /// The monitored selected object.
        /// </summary>
        private static Object _monitoredObject = null;

        /// <summary>
        /// The list of filtered history objects.
        /// </summary>
        private static readonly List<Object> _filteredHistoryObjects = new List<Object>();

        /// <summary>
        /// The property that is currently being edited.
        /// </summary>
        private static SerializedProperty _editingProperty;

        /// <summary>
        /// The search filter.
        /// </summary>
        private static string _searchFilter = "";

        /// <summary>
        /// The history objects data file name.
        /// </summary>
        private static readonly string _historyObjectsFile = "History Objects.data";

        /// <summary>
        /// Indicates whether the current cycle is a script reload.
        /// </summary>
        private static bool _scriptReload = false;

        public void OnGUI()
        {
            DrawWindowContents();
        }

        private void OnEnable()
        {
            IsOpen = true;
        }

        private void OnDisable()
        {
            IsOpen  = false;
            _window = null;
        }

        /// <summary>
        /// Shows the window.
        /// </summary>
        public static void ShowWindow()
        {
            _window = GetWindow<ObjectPickerHistoryWindow>("SOFlow-Object Picker History");
        }

        /// <summary>
        /// Draws the contents of the window.
        /// </summary>
        private void DrawWindowContents()
        {
            SOFlowEditorUtilities.DrawPrimaryLayer(() =>
                                                   {
                                                       SOFlowEditorUtilities.DrawTertiaryLayer(DrawHeader);

                                                       SOFlowEditorUtilities
                                                          .DrawScrollViewColourLayer(SOFlowEditorSettings.SecondaryLayerColour,
                                                                                     ref _scrollPosition,
                                                                                     DrawObjectList);
                                                   });
        }

        /// <summary>
        /// Draws the window header.
        /// </summary>
        private void DrawHeader()
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField($"Previously Selected {CurrentListingType}Objects", SOFlowStyles .BoldCenterTextHelpBox,
                                       GUILayout.Height(EditorGUIUtility.singleLineHeight * 2.5f));

            EditorGUILayout.BeginVertical(SOFlowStyles.CenterTextHelpBox, GUILayout.MaxWidth(200f));

            EditorGUILayout.LabelField("Search", SOFlowStyles.CenteredLabel, GUILayout.MaxWidth(200f));

            Color originalColor = GUI.backgroundColor;
            GUI.backgroundColor = SOFlowEditorSettings.SecondaryLayerColour;

            EditorGUI.BeginChangeCheck();
            
            _searchFilter = EditorGUILayout.TextField(_searchFilter, GUILayout.MaxWidth(200f)).Trim().ToLower();

            if(EditorGUI.EndChangeCheck())
            {
                UpdateObjectListing();
            }

            GUI.backgroundColor = originalColor;

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// Draws the object list.
        /// </summary>
        private void DrawObjectList()
        {
            for(int i = 0, condition = _filteredHistoryObjects.Count; i < condition; i++)
            {
                Object historyObject = _filteredHistoryObjects[i];

                if(historyObject != null)
                {
                    Texture2D buttonIcon =
                        (Texture2D)AssetDatabase.GetCachedIcon(AssetDatabase
                                                                  .GetAssetPath(historyObject));

                    if(buttonIcon == null)
                    {
                        buttonIcon = (Texture2D)EditorGUIUtility.ObjectContent(null, historyObject.GetType()).image;
                    }

                    if(buttonIcon != null)
                    {
                        buttonIcon = TextureExtensions.ResizeTexture(buttonIcon, 12, 12);
                    }

                    SOFlowEditorUtilities.DrawHorizontalColourLayer(SOFlowEditorSettings.SecondaryLayerColour,
                                                                    () =>
                                                                    {
                                                                        EditorGUILayout.LabelField($"{i}", SOFlowStyles.CenteredLabel, GUILayout.Width(18f));

                                                                        if(GUILayout
                                                                           .Button(new GUIContent(historyObject.name,
                                                                                                  buttonIcon),
                                                                                   EditorStyles.objectField))
                                                                        {
                                                                            if(EditingProperty != null)
                                                                            {
                                                                                EditingProperty.objectReferenceValue =
                                                                                    historyObject;

                                                                                EditingProperty
                                                                                   .serializedObject
                                                                                   .ApplyModifiedProperties();
                                                                            }
                                                                        }
                                                                    });
                }
            }
        }

        /// <summary>
        /// Updates the object listing.
        /// </summary>
        private static void UpdateObjectListing()
        {
            ObjectPickerHistoryObjects.ValidateListEntries(_object => _object);
            _filteredHistoryObjects.Clear();

            if(PropertyAvailable)
            {
                Type propertyType =
                    TypeExtensions.GetInstanceType(EditingProperty.type);

                if(propertyType != null)
                {
                    CurrentListingType = $"{propertyType.Name} ";
                    
                    _filteredHistoryObjects.AddRange(ObjectPickerHistoryObjects.FindAll(_object =>
                                                                                            _object
                                                                                               .GetType()
                                                                                               .IsSubclassOf(propertyType) ||
                                                                                            (propertyType
                                                                                                .IsSubclassOf(typeof(
                                                                                                                  Component
                                                                                                              )) &&
                                                                                             (_object as GameObject)
                                                                                           ?.GetComponent(propertyType) !=
                                                                                             null)));
                }
                else
                {
                    CurrentListingType = "";
                    _filteredHistoryObjects.AddRange(ObjectPickerHistoryObjects);
                }

                for(int i = _filteredHistoryObjects.Count - 1; i >= 0; i--)
                {
                    if(!_filteredHistoryObjects[i].name.ToLower().Contains(_searchFilter.ToLower()))
                    {
                        _filteredHistoryObjects.RemoveAt(i);
                    }
                }
            }
            else
            {
                CurrentListingType = "";
                _filteredHistoryObjects.AddRange(ObjectPickerHistoryObjects);
            }

            if(_scriptReload)
            {
                _scriptReload = false;

                return;
            }

            Window.Repaint();
        }

        /// <summary>
        /// Registers the object picker monitor method to the editor update event.
        /// </summary>
        [InitializeOnLoadMethod]
        private static void RegisterObjectPickerMonitor()
        {
            EditorApplication.update -= MonitorObjectPicker;
            EditorApplication.update += MonitorObjectPicker;
        }

        /// <summary>
        /// Monitors the object picker for any selected object.
        /// </summary>
        private static void MonitorObjectPicker()
        {
            if(focusedWindow?.ToString() == " (UnityEditor.ObjectSelector)")
            {
                _monitoringSelectedObject = true;
                _monitoredObject          = EditorGUIUtility.GetObjectPickerObject();
            }
            else if(_monitoringSelectedObject)
            {
                if(!ObjectPickerHistoryObjects.Contains(_monitoredObject))
                {
                    ObjectPickerHistoryObjects.Insert(0, _monitoredObject);
                }
                else
                {
                    ObjectPickerHistoryObjects.Remove(_monitoredObject);
                    ObjectPickerHistoryObjects.Insert(0, _monitoredObject);
                }

                if(IsOpen)
                {
                    UpdateObjectListing();
                }

                _monitoredObject          = null;
                _monitoringSelectedObject = false;
                
                SaveHistoryObjectsData();
            }
        }
        
        /// <summary>
        /// Saves the SOFlow history objects data.
        /// </summary>
        public static void SaveHistoryObjectsData()
        {
            StringBuilder historyObjectData = new StringBuilder();

            foreach(Object historyObject in ObjectPickerHistoryObjects)
            {
                string objectPath = AssetDatabase.GetAssetPath(historyObject);

                if(!string.IsNullOrEmpty(objectPath))
                {
                    historyObjectData.AppendLine(objectPath);
                }
            }

            try
            {
                File.WriteAllText(Path.Combine(Application.persistentDataPath, _historyObjectsFile), historyObjectData.ToString());
            }
            catch(Exception e)
            {
                Debug.LogError($"Failed to save history object data.\n\n{e.Message}");
            }
        }

        /// <summary>
        /// Loads the SOFlow history objects data.
        /// </summary>
        [UnityEditor.Callbacks.DidReloadScripts]
        [InitializeOnLoadMethod]
        public static void LoadHistoryObjectsData()
        {
            try
            {
                string filePath = Path.Combine(Application.persistentDataPath, _historyObjectsFile);

                if(File.Exists(filePath))
                {
                    ObjectPickerHistoryObjects.Clear();
                    
                    string[] historyObjectData = File.ReadAllLines(filePath);

                    foreach(string data in historyObjectData)
                    {
                        Object _historyObject = AssetDatabase.LoadAssetAtPath<Object>(data);

                        if(_historyObject != null)
                        {
                            ObjectPickerHistoryObjects.Add(_historyObject);
                        }
                    }
                }
                
                if(IsOpen)
                {
                    _scriptReload = true;
                    UpdateObjectListing();
                }
            }
            catch(Exception e)
            {
                Debug.LogError($"Failed to load history object data.\n\n{e.Message}");
            }
        }
    }
}
#endif