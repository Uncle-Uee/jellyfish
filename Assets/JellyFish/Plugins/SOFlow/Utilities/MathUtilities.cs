// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

namespace SOFlow.Utilities
{
    public static class MathUtilities
    {
	    /// <summary>
	    ///     Returns the value between min and max for the given percentage.
	    /// </summary>
	    /// <param name="percentage"></param>
	    /// <param name="min"></param>
	    /// <param name="max"></param>
	    /// <returns></returns>
	    public static float GetValueFromPercentage(float percentage, float min, float max)
        {
            return max - percentage * (max - min);
        }

	    /// <summary>
	    ///     Returns the percentage between min and max for the given value.
	    /// </summary>
	    /// <param name="value"></param>
	    /// <param name="min"></param>
	    /// <param name="max"></param>
	    /// <returns></returns>
	    public static float GetPercentageFromValue(float value, float min, float max)
        {
            return (value - min) / (max - min);
        }
    }
}