/**
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

        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(value + " ");
        foreach (object element in values)
        {
            if (element.GetType().IsArray)
            {
                stringBuilder.Append((element as object[]).Display());
            }
            else
            {
                stringBuilder.Append(element);
            }
        }

        return stringBuilder.ToString();
    }
}