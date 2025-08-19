using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace System.Collections.Generic
{
    public static class IListExtension
    {
        /// <summary>
        /// Removes all the elements that match the conditions defined by the specified predicate.
        /// </summary>
        /// <param name="list">The list to remove from.</param>
        /// <param name="match">The predicate that defines the conditions of the elements to remove.</param>
        /// <typeparam name="T">The type of list.</typeparam>
        public static void RemoveAll<T>(this IList<T> list, Predicate<T> match)
        {
            if (list is List<T> li)
            {
                li.RemoveAll(match);
                return;
            }
        
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (match(list[i]))
                {
                    list.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Performs a Fisher-Yates shuffle on the list.
        /// </summary>
        /// <param name="list">The list to shuffle.</param>
        /// <typeparam name="T">The type of list.</typeparam>
        public static void Shuffle<T>(this IList<T> list)
        {
            int count = list.Count;
            for (var i = 0; i < count - 1; ++i)
            {
                int r = UnityEngine.Random.Range(i, count);
                (list[i], list[r]) = (list[r], list[i]);
            }
        }
    }
}