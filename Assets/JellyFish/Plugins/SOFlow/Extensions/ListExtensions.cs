// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace SOFlow.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        ///     Swaps the order of the provided indices of the list.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index0"></param>
        /// <param name="index1"></param>
        /// <typeparam name="T"></typeparam>
        public static void Swap<T>(this List<T> list, int index0, int index1)
        {
            T swappedItem = list[index0];
            list[index0] = list[index1];
            list[index1] = swappedItem;
        }

        /// <summary>
        ///     Removes any null entries from the list.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="entryExistenceCheck"></param>
        /// <typeparam name="T"></typeparam>
        public static void ValidateListEntries<T>(this List<T> list, Func<T, bool> entryExistenceCheck = null)
        {
            List<int> entriesToRemove = new List<int>();

            for(int i = 0; i < list.Count; i++)
                if(list[i] == null || entryExistenceCheck != null && !entryExistenceCheck.Invoke(list[i]))
                    entriesToRemove.Add(i);

            for(int i = entriesToRemove.Count - 1; i >= 0; i--) list.RemoveAt(entriesToRemove[i]);
        }

        /// <summary>
        ///     Shuffles the list.
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        public static void Shuffle<T>(this IList<T> list)
        {
            for(int i = 0, count = list.Count, last = count - 1; i < last; ++i)
            {
                int random = Random.Range(i, count);
                T   temp   = list[i];
                list[i]      = list[random];
                list[random] = temp;
            }
        }
    }
}