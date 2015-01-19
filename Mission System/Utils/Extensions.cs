using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public static class Extensions {

	public static System.Random randomInt;

	public static void MoveNextCycle(this IEnumerator en)
	{
		/// <summary>
		/// Moves to the next element in an enumerable array and resets the enumerator if there is no next element
		/// </summary>
		if(!en.MoveNext())
		{
			en.Reset();
			en.MoveNext();
		}
	}

	public static T GetRandomElement<T>(this T[] array)
	{
		/// <summary>
		/// Returns random element from an array using System.Random
		/// </summary>
		if(array.Length < 1)
			return default(T);

		if(randomInt == null)
			randomInt = new System.Random();

		return array[randomInt.Next(0, array.Length)];
	}

	public static T GetRandomElement<T>(this List<T> list)
	{
		/// <summary>
		/// Returns random element from a list using System.Random
		/// </summary>
		if(list.Count < 1)
			return default(T);

		if(randomInt == null)
			randomInt = new System.Random();

		return list[randomInt.Next(0, list.Count)];
	}

	public static int GetRandomArrayPosition<T>(this T[] array)
	{
		/// <summary>
		/// Returns random position from an array using System.Random
		/// </summary>
		if(array.Length < 1)
			return -1;

		if(randomInt == null)
			randomInt = new System.Random();
		
		return randomInt.Next(0, array.Length);
	}
	
	public static int GetRandomListPosition<T>(this List<T> list)
	{
		/// <summary>
		/// Returns random position from a list using System.Random
		/// </summary>
		if(list.Count < 1)
			return -1;

		if(randomInt == null)
			randomInt = new System.Random();
		
		return randomInt.Next(0, list.Count);
	}

}
