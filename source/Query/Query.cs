using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using static AHKCore.Nodes;

namespace AHKCore
{
	public static class Query
	{
		public static string Flatten<T>(this List<T> l, string delimiter = null)
		{
			StringBuilder sb = new StringBuilder();
			foreach (var v in l)
				sb.Append(sb.Length == 0 ? v.ToString() : delimiter + v.ToString());
			
			return sb.ToString();
		}

		public static string Indent<T>(this string s)
		{
			return string.Join("\n\t", s.Split("\n"));
		}
	}
}