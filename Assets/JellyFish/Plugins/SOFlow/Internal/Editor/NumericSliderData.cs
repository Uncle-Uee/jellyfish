// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

#if UNITY_EDITOR
using System;
using System.Collections.Generic;

namespace SOFlow.Internal
{
	[Serializable]
	public class NumericSliderData
	{
		/// <summary>
		/// The slider ID.
		/// </summary>
		public int SliderID = -1;
		
		/// <summary>
		/// Indicates whether the numeric slider is active.
		/// </summary>
		public bool SliderActive = false;

		/// <summary>
		/// The minimum value of the slider.
		/// </summary>
		public float SliderMinValue = 0f;

		/// <summary>
		/// The maximum value of the slider.
		/// </summary>
		public float SliderMaxValue = 1f;
	}

	[Serializable]
	public class NumericSliderList
	{
		/// <summary>
		/// The slider data.
		/// </summary>
		public List<NumericSliderData> SliderData = new List<NumericSliderData>();
	}
}
#endif