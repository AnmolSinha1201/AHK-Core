using System.Collections.Generic;
using static AHKCore.Nodes;

namespace AHKCore.IndexedNodesFragment
{
	class Functions
	{
		Dictionary<string, List<functionDeclarationClass>> FunctionList = new Dictionary<string, List<functionDeclarationClass>>();

		/*
			- No setter as getter handles adding new List if function name does not exist. Therefore, call Functions["name"].Add(func)
			- TODO: Add extension method to get proper function signature (Regular, Variadic, not found, multiple signature found)
		 */

		public List<functionDeclarationClass> this[string key]
		{
			get
			{
				if (!FunctionList.ContainsKey(key.ToLower()))
					FunctionList.Add(key.ToLower(), new List<functionDeclarationClass>());
				return FunctionList[key.ToLower()];
			}
		}
	}
}