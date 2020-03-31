// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System.Text;

namespace SOFlow.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        ///     The string builder instance.
        /// </summary>
        private static StringBuilder _stringBuilderInstance;

        /// <summary>
        ///     The string builder reference.
        /// </summary>
        private static StringBuilder _stringBuilder
        {
            get
            {
                if(_stringBuilderInstance == null) _stringBuilderInstance = new StringBuilder();

                return _stringBuilderInstance;
            }
        }

        /// <summary>
        ///     A high performance string concatenation alternative.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string Combine(this string value, params object[] values)
        {
            int valuesLength = values.Length;

            // The string builder only really adds a performance gain when large numbers of strings are being combined.
            // So we'll use normal string interpolation for smaller combinations.
            if(valuesLength == 0) return value;

            if(valuesLength == 1) return $"{value}{values[0]}";

            if(valuesLength == 2) return $"{value}{values[0]}{values[1]}";

            _stringBuilder.Clear();

            _stringBuilder.Append(value);

            foreach(string _value in values) _stringBuilder.Append(_value);

            return _stringBuilder.ToString();
        }
    }
}