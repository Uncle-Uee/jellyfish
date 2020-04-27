/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

/// <summary>
///     Extend the List Class.
/// </summary>
public static class ListExtension
{
    /// <summary>
    /// Random Instance Reference.
    /// </summary>
    private static Random Random { get; } = new Random();

    /// <summary>
    ///     Shuffling Algorithm Based on Knuth Fisher Yates Shuffle.
    /// </summary>
    /// <example>
    ///     <code>
    /// List<T>
    ///             list = new List
    ///             <T>
    ///                 (){ 1, 2, 3};
    ///                 list.Shuffle();
    /// </code>
    /// </example>
    /// <param name="list"></param>
    /// <typeparam name="T"></typeparam>
    public static void Shuffle<T>(this List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list.Swap(i, i + Random.Next(list.Count - i));
        }
    }

    /// <summary>
    ///     Swap 2 Elements at Index i and Index j.
    /// </summary>
    /// <param name="list"></param>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>
    ///     The list with the newly Swapped Elements.
    /// </returns>
    private static void Swap<T>(this List<T> list, int i, int j)
    {
        T element = list[i];
        list[i] = list[j];
        list[j] = element;
    }


    /// <summary>
    /// Display List Items.
    /// </summary>
    /// <param name="list"></param>
    /// <param name="log"></param>
    public static void Display<T>(this List<T> list)
    {
        string output = "";

        for (int i = 0; i < list.Count; i++)
        {
            output = output.Combine(list[i].ToString(), " ");
        }

        Debug.Log(output);
    }

    /// <summary>
    /// Removes any null entries from the list.
    /// </summary>
    /// <param name="list"></param>
    /// <param name="entryExistenceCheck"></param>
    /// <typeparam name="T"></typeparam>
    public static void ValidateListEntries<T>(this List<T> list, Func<T, bool> entryExistenceCheck = null)
    {
        List<int> entriesToRemove = new List<int>();

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == null || entryExistenceCheck != null && !entryExistenceCheck.Invoke(list[i]))
            {
                entriesToRemove.Add(i);
            }
        }

        for (int i = entriesToRemove.Count - 1; i >= 0; i--)
        {
            list.RemoveAt(entriesToRemove[i]);
        }
    }
}