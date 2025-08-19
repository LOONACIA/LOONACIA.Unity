using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace System.Collections.Generic
{
	public static class IEnumerableExtension
	{
		/// <summary>
		/// Returns an empty collection if the source is null.
		/// </summary>
		/// <returns>An empty collection if the source is null. Otherwise, the source.</returns>
		public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source)
		{
			return source ?? Enumerable.Empty<T>();
		}
		
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}
			
			foreach (var item in source)
			{
				action(item);
			}
		}
		
		public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException(nameof(action));
			}
			
			int index = 0;
			foreach (var item in source)
			{
				action(item, index++);
			}
		}
	}
}
