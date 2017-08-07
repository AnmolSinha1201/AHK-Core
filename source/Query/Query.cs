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
		public static List<T2> OfTypeRecursive<T1, T2>(this List<T1> l, T2 type)
		{
			var retList = l.OfType<T2>().ToList();
			
			foreach (var v in l)
				if(v.GetType() == typeof(loopLoopClass))
					retList.AddRange(((loopLoopClass)(object)v).loopBody.OfTypeRecursive(type));
			
			return retList;
		}

	}
}