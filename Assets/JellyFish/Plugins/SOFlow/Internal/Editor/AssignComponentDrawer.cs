// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace SOFlow.Internal
{
	[CustomPropertyDrawer(typeof(AssignComponentAttribute))]
	public class RequireComponentDrawer : PropertyDrawer
	{
		/// <summary>
		/// Indicates whether the target component type can be assigned to the field value.
		/// </summary>
		private bool _fieldAssignable = false;

		/// <summary>
		/// Indicates whether the property drawer has been initialized.
		/// </summary>
		private bool _initialized = false;

		/// <summary>
		/// The component type to assign to the field value. 
		/// </summary>
		private Type _assigningType = null;

		/// <summary>
		/// Indicates whether components should be searched for in child objects.
		/// </summary>
		private bool _searchChildren = false;
		
		/// <inheritdoc />
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if(!_initialized)
			{
				AssignComponentAttribute assignAttribute = attribute as AssignComponentAttribute;

				_searchChildren = assignAttribute.SearchChildren;
				
				if(assignAttribute.AssigningType != null)
				{
					_assigningType = assignAttribute.AssigningType;
				}
				else
				{
					_assigningType = fieldInfo.FieldType;
				}
				
				_fieldAssignable = fieldInfo.FieldType.IsSubclassOf(typeof(Component));

				if(!_fieldAssignable)
				{
					Debug.LogWarning($"[AssignComponent] : Provided type ({fieldInfo.FieldType}) cannot be assigned. Type must derive from (Component).");
				}

				_initialized = true;
			}

			if(_fieldAssignable)
			{
				Component componentReference = property.serializedObject.targetObject as Component;
				Component attachedComponent = null;

				if(_searchChildren)
				{
					attachedComponent = componentReference.gameObject.GetComponentInChildren(_assigningType);
				}
				else
				{
					attachedComponent = componentReference.gameObject.GetComponent(_assigningType);
				}

				if(attachedComponent)
				{
					ApplyComponentReference(componentReference, attachedComponent);
				}
				else
				{
					attachedComponent = componentReference.gameObject.AddComponent(_assigningType);
					
					ApplyComponentReference(componentReference, attachedComponent);
				}
			}

			EditorGUI.BeginProperty(position, label, property);
			EditorGUI.PropertyField(position, property, label);
			EditorGUI.EndProperty();
		}

		/// <summary>
		/// Applies the target component to the component reference.
		/// </summary>
		/// <param name="componentReference"></param>
		/// <param name="targetComponent"></param>
		private void ApplyComponentReference(Component componentReference, Component targetComponent)
		{
			object fieldReference = fieldInfo.GetValue(componentReference);

			if(fieldReference == null || fieldReference.Equals(null))
			{
				fieldInfo.SetValue(componentReference, targetComponent);
			}
		}
	}
}
#endif