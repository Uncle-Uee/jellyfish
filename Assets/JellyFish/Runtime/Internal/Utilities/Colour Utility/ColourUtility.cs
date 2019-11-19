/*
 *
 * Created By: Ubaidullah Effendi-Emjedi, Uee
 * Alias: Uee
 * Modified By:
 *
 * Last Modified: 09 November 2019
 *
 */

using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.Internal.Utilities
{
    public static class ColorUtility
    {
        #region PROPERTIES

        public static Color Pink       => new Color(1f, 0.41568627450980394f, 1f);
        public static Color CerisePink => new Color(1f, 0f, 0.4313725490196078f);
        public static Color PastelPink => new Color(1f, 0.4980392156862745f, 0.7137254901960784f);
        public static Color Lavender   => new Color(0.6313725490196078f, 0.4980392156862745f, 1f);
        public static Color Purple     => new Color(.6980392156862745f, 0f, 1f);
        public static Color Grape      => new Color(0.2823529411764706f, 0f, 1f);
        public static Color SoftRed    => new Color(1f, 0.4980392156862745f, 0.4980392156862745f);
        public static Color Navy       => new Color(0f, 0.14901960784313725f, 1f);
        public static Color SkyBlue    => new Color(0f, 0.5803921568627451f, 1f);
        public static Color BabyBlue   => new Color(0.4980392156862745f, 0.788235294117647f, 1f);
        public static Color Turquoise  => new Color(0f, 1f, 1f);
        public static Color Teal       => new Color(0f, 0.4980392156862745f, 0.27450980392156865f);
        public static Color LiteTeal   => new Color(0f, 1f, 0.5647058823529412f);
        public static Color Lime       => new Color(0.7137254901960784f, 1f, 0f);
        public static Color Mustard    => new Color(0.9686274509803922f, 0.807843137254902f, 0f);
        public static Color Orange     => new Color(1f, 0.41568627450980394f, 0f);
        public static Color Peach      => new Color(1f, 0.6980392156862745f, 0.4980392156862745f);
        public static Color Brown      => new Color(0.4980392156862745f, 0.2f, 0f);
        public static Color Tan        => new Color(0.7490196078431373f, 0.2980392156862745f, 0f);

        #endregion

        #region COLOR UTILITY METHODS

        /// <summary>
        /// A New Color based on the 255 Range of Values.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Color Color255(float r, float g, float b, float a = 255f)
        {
            return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
        }

        #endregion
    }
}