﻿/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using System.Text;

public static class StringExtension
{
    /// <summary>
    ///     Concat String Data using String Builder.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static string Combine(this string value, params object[] values)
    {
        if (values.Length == 0)
            return value;

        if (values.Length == 1 && !values[0].GetType().IsArray)
        {
            return value + " " + values[0];
        }

        if (values.Length > 1 && values.Length <= 1000)
        {
            string output = value;

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i].GetType().IsArray)
                {
                    (values[i] as object[]).Display(ref output);
                }
                else
                {
                    output = string.Concat(output, values[i].ToString());
                }
            }

            return output;
        }

        // Clear String Builder.
        StringBuilder stringBuilder = new StringBuilder();

        // Add the Value Before Concatenating.
        stringBuilder.Append(value + " ");

        for (int i = 0; i < values.Length; i++)
        {
            if (values[i].GetType().IsArray)
            {
                stringBuilder.Append((values[i] as object[]).Display());
            }
            else
            {
                stringBuilder.Append(values[i]);
            }
        }

        return stringBuilder.ToString();
    }
}