﻿using System.Collections.Generic;

namespace KsWare.CaliburnMicro.Extensions
{
	public static class IListExtension
	{
		public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
		{
			foreach (var item in items)  list.Add(item); 
		}
		public static void RemoveRange<T>(this IList<T> list, IEnumerable<T> items)
		{
			foreach (var item in items) list.Remove(item);
		}
	}
}
