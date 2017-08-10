using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using static AHKCore.BaseVisitor;

namespace AHKCore
{
	public static class Query
	{
		public static List<T> OfTypeRecursive<T>(this List<object> list) where T : ISearchable
		{
			var retList = list.OfType<T>().ToList();
			
			foreach (var v in list.Except(retList.Cast<object>()))
				retList.AddRange(((ISearchable)v).Searchables.OfTypeRecursive<T>());

			return retList;
		}

		public static string Flatten<T>(this List<T> l, string delimiter = null)
		{
			StringBuilder sb = new StringBuilder();
			foreach (var v in l)
				sb.Append(sb.Length == 0 ? v.ToString() : delimiter + v.ToString());
			
			return sb.ToString();
		}

		public static IEnumerable<T> AddConcat<T>(this List<T> l, object o)
		{
			if (o.GetType() == typeof(List<T>))
				return l.Concat((List<T>)o);

			l.Add((T)o);
			return l;
		}

	}
}