/*
 *
 * Created By: Ubaidullah Effendi-Emjedi, Uee
 * Alias: Uee
 * Modified By:
 *
 * Last Modified: 12 November 2019
 *
 */

using System;

// ReSharper disable once CheckNamespace
namespace JellyFish.Internal.Utilities
{
    /// <summary>
    /// Recast an Object Type Value to its Correct Possible Type such as int, string, byte, etc.
    /// </summary>
    public static class RecastUtility
    {
        /// <summary>
        /// Recast an object to its correct strongly typed value.
        /// </summary>
        /// <param name="object"></param>
        /// <param name="value"></param>
        public static void RecastObject<T>(object @object, out dynamic value)
        {
            if (@object is T)
            {
                value = (T) @object;
            }
            else
            {
                throw new Exception("Can not Recast Object!");
            }
        }

        /// <summary>
        /// Recast an object to a strongly typed value.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T RecastObject<T>(object value)
        {
            T typedValue = (T) value;

            if (typedValue != null)
            {
                return typedValue;
            }

            return default;
        }


        /// <summary>
        /// Convert a String value into the Correct Strongly Typed Value.
        /// </summary>
        /// <param name="value"></param>
        public static dynamic ConvertString(string value)
        {
            if (bool.TryParse(value, out bool @bool))
            {
                return @bool;
            }

            if (byte.TryParse(value, out byte @byte))
            {
                return @byte;
            }

            if (sbyte.TryParse(value, out sbyte @sbyte))
            {
                return @sbyte;
            }

            if (float.TryParse(value, out float @float))
            {
                return @float;
            }

            if (double.TryParse(value, out double @double))
            {
                return @double;
            }

            if (int.TryParse(value, out int @int))
            {
                return @int;
            }

            if (uint.TryParse(value, out uint @uint))
            {
                return @uint;
            }

            if (long.TryParse(value, out long @long))
            {
                return @long;
            }

            if (ulong.TryParse(value, out ulong @ulong))
            {
                return @ulong;
            }

            if (short.TryParse(value, out short @short))
            {
                return @short;
            }

            if (ushort.TryParse(value, out ushort @ushort))
            {
                return @ushort;
            }

            return default;
        }
    }
}