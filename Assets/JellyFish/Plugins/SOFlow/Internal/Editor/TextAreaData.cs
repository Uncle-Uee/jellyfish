// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using System;
using System.Collections.Generic;

namespace SOFlow.Internal
{
	[Serializable]
	public class TextAreaData
	{
		/// <summary>
		/// Indicates whether a text area is currently being dragged.
		/// </summary>
		public static bool TextAreaDragged = false;
		
		/// <summary>
		/// The text area ID.
		/// </summary>
		/// <returns></returns>
		public int TextAreaID = -1;

		/// <summary>
		/// Indicates whether the text area is active.
		/// </summary>
		public bool TextAreaActive = false;

		/// <summary>
		/// The text area height.
		/// </summary>
		public float TextAreaHeight = 1f;

		/// <summary>
		/// Indicates whether this text area is currently being dragged.
		/// </summary>
		[NonSerialized]
		public bool CurrentlyDragged = false;
	}

	[Serializable]
	public class TextAreaList
	{
		/// <summary>
		/// The text area data.
		/// </summary>
		public List<TextAreaData> TextAreaData = new List<TextAreaData>();
	}
}
#endif