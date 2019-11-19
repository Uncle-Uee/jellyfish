/*
 *
 * Created By: Ubaidullah Effendi-Emjedi
 * Alias: Uee
 * Modified By: 
 *
 * Last Modified: 20 June 2019
 *
 *
 * This software is released under the terms of the
 * GNU license. See https://www.gnu.org/licenses/#GPL
 * for more information.
 *
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
	public static void Display<T>(this List<T> list, bool log = true)
	{
		string output = "";
		for (int i = 0; i < list.Count; i++)
		{
			output = output.Combine(list[i].ToString(), " ");
		}

		if (log)
		{
			Debug.Log(output);
		}
		else
		{
			Console.WriteLine(output);
		}
	}
}