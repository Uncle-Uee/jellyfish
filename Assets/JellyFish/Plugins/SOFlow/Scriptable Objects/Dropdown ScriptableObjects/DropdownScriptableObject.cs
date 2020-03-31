// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System;
using System.Collections.Generic;
using SOFlow.Data.Primitives;
using UnityEngine;

namespace SOFlow.ScriptableObjects
{
	public class DropdownScriptableObject : ScriptableObject
	{
		public virtual void OnValidate()
		{
			RegisterDropdownEntry();
		}

		public virtual void Awake()
		{
			RegisterDropdownEntry();
		}

		public virtual void OnEnable()
		{
			RegisterDropdownEntry();
		}

		/// <summary>
		/// Registers this dropdown ScriptableObject to the available dropdowns list. 
		/// </summary>
		public void RegisterDropdownEntry()
		{
#if UNITY_EDITOR
			List<ScriptableObject> dropdowns;
			Type                   type = GetType();
			
			if(!DropdownScriptableObjectAttributeDrawer.AvailableDropdowns.TryGetValue(type, out dropdowns))
			{
				dropdowns = new List<ScriptableObject>
				            {
					            this
				            };

				DropdownScriptableObjectAttributeDrawer.AvailableDropdowns.Add(type, dropdowns);
			}
			else
			{
				if(!dropdowns.Contains(this))
				{
					dropdowns.Add(this);
					DropdownScriptableObjectAttributeDrawer.AvailableDropdowns[type] = dropdowns;
				}
			}

			// Explicitly add the PrimitiveData to the dropdown listing.
			if(type.IsSubclassOf(typeof(PrimitiveData)) && !DropdownScriptableObjectAttributeDrawer.AvailableDropdowns.ContainsKey(typeof(PrimitiveData)))
			{
				dropdowns = new List<ScriptableObject>
				            {
					            this
				            };

				DropdownScriptableObjectAttributeDrawer.AvailableDropdowns.Add(typeof(PrimitiveData), dropdowns);
			}

			foreach(KeyValuePair<Type, List<ScriptableObject>> dropdownData in DropdownScriptableObjectAttributeDrawer
			   .AvailableDropdowns)
			{
				if(dropdownData.Key != type && type.IsSubclassOf(dropdownData.Key))
				{
					if(!dropdownData.Value.Contains(this))
					{
						dropdownData.Value.Add(this);
					}
				}
			}
#endif
		}
	}
}