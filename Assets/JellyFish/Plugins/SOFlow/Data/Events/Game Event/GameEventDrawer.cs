// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using Pather.CSharp;
using SOFlow.Internal;
using UnityEditor;
using UnityEngine;

namespace SOFlow.Data.Events.Editor
{
	[CustomPropertyDrawer(typeof(GameEvent))]
	public class GameEventDrawer : PropertyDrawer
	{
		/// <summary>
		/// The resolver instance used to capture the associated Game Event instance.
		/// </summary>
		private Resolver _resolver = new Resolver();
		
		/// <inheritdoc />
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if(SOFlowEditorSettings.DrawDefaultProperties)
			{
				EditorGUI.PropertyField(position, property, label);
			}
			else
			{
				position = EditorGUI.PrefixLabel(position, label);

				float positionWidth = position.width;
				Color originalColour = GUI.backgroundColor;
				GUI.backgroundColor = SOFlowEditorSettings.AcceptContextColour;
				
				position.width = 42f;
				
				if(GUI.Button(position, "Raise", SOFlowStyles.Button))
				{
					if(!property.propertyPath.Contains("Array"))
					{
						GameEvent gameEvent =
							(GameEvent)_resolver.Resolve(property.serializedObject.targetObject, property.propertyPath);

						gameEvent?.Raise();
					}
					else
					{
						GameEvent gameEvent =
							(GameEvent)_resolver.Resolve(property.serializedObject.targetObject, property.propertyPath
							                                                                             .Replace(".Array.data",
							                                                                                      ""));

						gameEvent?.Raise();
					}
				}

				GUI.backgroundColor = originalColour;

				position.x += 45f;
				position.width = positionWidth - 45f;

				EditorGUI.PropertyField(position, property, GUIContent.none);
			}
		}
	}
}
#endif